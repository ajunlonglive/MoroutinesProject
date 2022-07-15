using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Redcode.Moroutines.Moroutine;

namespace Redcode.Moroutines
{
    public class MoroutinesGroup
    {
        private readonly List<Moroutine> _moroutines = new();

        public int Count => _moroutines.Count;

        public Owner Owner => _moroutines.Distinct().Count() == 1 ? _moroutines[0].Owner : null;

        #region State
        public bool IsReseted => _moroutines.All(m => m.IsReseted);

        public bool IsRunning => _moroutines.All(m => m.IsRunning);

        public bool IsStopped => _moroutines.All(m => m.IsStopped);

        public bool IsCompleted => _moroutines.All(m => m.IsCompleted);

        public bool IsDestroyed => _moroutines.All(m => m.IsDestroyed);
        #endregion

        public bool AutoDestroy
        {
            get => _moroutines.All(m => m.AutoDestroy);
            set => _moroutines.ForEach(m => m.AutoDestroy = value);
        }

        #region Events
        /// <summary>
        /// The event that is redeemed when the group resets all moroutines to its initial state.
        /// Triggered after <see cref="Reset"/> method called.
        /// </summary>
        public event Action<MoroutinesGroup> Reseted;

        /// <summary>
        /// The event that is redeemed when the group starts performing.
        /// Triggered after <see cref="Run"/> method called.
        /// </summary>
        public event Action<MoroutinesGroup> Running;

        /// <summary>
        /// The event that is redeemed when the group stops executing.
        /// Triggered after <see cref="Stop"/> method called.
        /// </summary>
        public event Action<MoroutinesGroup> Stopped;

        /// <summary>
        /// The event that is emit when the moroutines destroyed.
        /// Triggered after <see cref="Destroy"/> method called.
        /// </summary>
        public event Action<MoroutinesGroup> Destroyed;
        #endregion

        public Moroutine this[int index] => _moroutines[index];

        #region Constructors
        public MoroutinesGroup() { }

        public MoroutinesGroup(params Moroutine[] moroutines) : this((IEnumerable<Moroutine>)moroutines) { }

        public MoroutinesGroup(IEnumerable<Moroutine> moroutines) => _moroutines.AddRange(moroutines);
        #endregion

        #region Owning
        public List<Moroutine> GetUnownedMoroutines()
        {
            return GetUnownedMoroutines(State.Reseted | State.Running | State.Stopped | State.Completed | State.Destroyed);
        }

        public List<Moroutine> GetUnownedMoroutines(State mask)
        {
            var unowned = Moroutine.GetUnownedMoroutines(mask);
            return unowned.Where(m => _moroutines.Contains(m)).ToList();
        }

        public void SetOwner(Component component) => SetOwner(component.gameObject);

        public void SetOwner(GameObject gameObject) => _moroutines.ForEach(mor => mor.SetOwner(gameObject));

        public void MakeUnowned() => _moroutines.ForEach(m => m.MakeUnowned());
        #endregion

        #region Control
        public MoroutinesGroup Reset()
        {
            _moroutines.ForEach(m => m.Reset());
            Reseted?.Invoke(this);

            return this;
        }

        public MoroutinesGroup Run()
        {
            _moroutines.ForEach(m => m.Run());
            Running?.Invoke(this);

            return this;
        }

        public MoroutinesGroup Stop()
        {
            _moroutines.ForEach(m => m.Stop());
            Stopped?.Invoke(this);

            return this;
        }

        public MoroutinesGroup Rerun()
        {
            Reset();
            Run();

            return this;
        }
        #endregion

        #region Subscribing
        private MoroutinesGroup OnSubscribe(ref Action<MoroutinesGroup> ev, Action<MoroutinesGroup> action)
        {
            ev += action;
            return this;
        }

        /// <summary>
        /// Subscribe to reset event.
        /// </summary>
        /// <param name="action">Callback to invoke.</param>
        /// <returns>The moroutines group.</returns>
        public MoroutinesGroup OnReseted(Action<MoroutinesGroup> action) => OnSubscribe(ref Reseted, action);

        /// <summary>
        /// Subscribe to run event.
        /// </summary>
        /// <param name="action"><inheritdoc cref="OnReseted(Action{Moroutine})"/></param>
        /// <returns>The moroutines group.</returns>
        public MoroutinesGroup OnRunning(Action<MoroutinesGroup> action) => OnSubscribe(ref Running, action);

        /// <summary>
        /// Subscribe to stop event.
        /// </summary>
        /// <param name="action"><inheritdoc cref="OnReseted(Action{Moroutine})"/></param>
        /// <returns>The moroutines group.</returns>
        public MoroutinesGroup OnStopped(Action<MoroutinesGroup> action) => OnSubscribe(ref Stopped, action);
        #endregion

        #region Yielders
        public WaitForAll WaitForCompleted() => new(_moroutines);

        public WaitForAll WaitForStop() => new(_moroutines.Select(m => m.WaitForStop()));

        public WaitForAll WaitForRun() => new(_moroutines.Select(m => m.WaitForRun()));

        public WaitForAll WaitForReset() => new(_moroutines.Select(m => m.WaitForReset()));

        public WaitForAll WaitForDestroy() => new(_moroutines.Select(m => m.WaitForDestroy()));
        #endregion

        #region Destroying
        /// <summary>
        /// Stop and destroy moroutines immediatly. You can't run moroutines after it destroying.
        /// </summary>
        public MoroutinesGroup Destroy()
        {
            _moroutines.ForEach(m => m.Destroy());
            Destroyed.Invoke(this);

            return this;
        }

        /// <summary>
        /// Sets moroutines auto destroying.
        /// </summary>
        /// <param name="autoDestroy"><see langword="true"/> if you need destroy moroutines after completion.</param>
        /// <returns>The moroutine.</returns>
        public MoroutinesGroup SetAutoDestroy(bool autoDestroy)
        {
            _moroutines.ForEach(m => m.AutoDestroy = autoDestroy);
            return this;
        }
        #endregion
    }
}
