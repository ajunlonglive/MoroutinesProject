# Moroutines
Moroutines - More Than Coroutines, это библиотека C# написанная для работы с корутинами в Unity. 

Unity по умолчанию предоставляет возможность работы с корутинами, однако этот подход имеет недостатки. С помощью этой библиотеки мы попытались обойти эти недостатки 
предлагая вам собственное API для работы с корутинами. Тем не менее, наша библиотека используется Unity-корутины чтобы организовать псевдопараллельное выполнение ваших методов.
Вы можете использовать одновременно как встроенный подход для работы с корутинами, так и нашу библиотеку.

### Почему морутина?
В Unity уже существует класс `Coroutine` для работы с корутинами и у нас было несколько путей реализации нашей библиотеки. Мы решили идти по пути наименьшего сопротивления и ровно по этой причине в нашей библиотеке корутины называются иначе - морутины. Это позволяет вам легко использовать как встроенные в Unity корутины используя класс `Coroutine`, так и более продвинутые морутины из нашей библиотеки используя класс `Moroutine`.

### Какие преимущества?
Встроенный подход работы с корутинами имеет несколько недостатков:
- Одну корутину может ожидать только другая одна корутина, в противном случае (если ожидающих больше одной), Unity сообщит вам об ошибке в консоли в процессе выполнения игры.
- Нет возможности по объекту класса `Coroutine` узнать о состоянии корутины (обнулена, выполняется, приостановлена, завершена или уничтожена).
- Нет возможности приостановить или перезапустить корутину по объекту класса `Coroutine`.
- Нет возможности создать корутину с отложенным запуском.
- Нет возможности ожидания паузы или возобнавления корутины.
- Нет возможности подписаться на события изменения состояния корутины.
- Нет возможности получить последний результат корутины.
- Нет возможности запустить несколько корутин одним методом.
- Нет возможности ожидать окончания выполнения нескольких корутин сразу.
- Нет возможности ожидать окончания выполнения хотя бы одной из нескольких корутин сразу.
- Нет возможности получить все корутины конкретного игрового объекта.
- И прочие...

Наша библиотека исключена недостатков перечисленных выше. Вы можете легко управлять корутиной всего парой строчек кода, определять ее состояние, реагировать на события и так далее.

### Импорт библиотеки
Вы можете импортировать нашу библиотеку с помощью магазина Asset Store или скачав Unity-package отсюда. 
> Не извлекайте содержимое папки `Plugins` в другое место, это сделает некоторые `internal` классы доступными для вас, что может привести к ошибкам в будущем. 

### Подключение пространства имен
Для работы с морутинами необходимо подключить пространство имен `Redcode.Moroutines`. В этом пространстве находятся все типы данных которые мы создали для работы с морутинами.
```c#
using Redcode.Moroutines;
```
После этого вы можете использовать класс `Moroutine` из данной библиотеки для работы с корутинами.

### Создание продвинутой корутины
Чтобы создать корутину вам нужен объект `IEnumerator`. Как вы возможно знаете, такой объект проще всего создать с помощью инструкции `yield` внутри метода возвращающего `IEnumerator`.
```c#
private IEnumerator TickEnumerator()
{
    while (true)
    {
        yield return new WaitForSeconds(1f);
        print("Tick!");
    }
}
```
В примере выше объявлен метод в котором бесконечно с секундной задержкой выводится текст "Tick!" в консоль Unity. Чтобы запустить его встроенным в Unity способом вы бы использовали метод `StartCoroutine`, однако данный метод вернул бы вам объект `UnityEngine.Coroutine`, который не предоставляет никакой информации о состоянии корутины. Вместо этого, вы должны использовать метод `Moroutine.Create`.
```c#
Moroutine.Create(TickEnumerator());
```
В этом случае статический метод `Moroutine.Create` вернет вам морутину с множеством методов и свойств для работы с ней. В целом скрипт с вышеперечисленными примерами кода будет выглядеть так.
```c#
using System.Collections;
using UnityEngine;
using Redcode.Moroutines;

public class Test : MonoBehaviour
{
    private void Start() => Moroutine.Create(TickEnumerator());

    private IEnumerator TickEnumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            print("Tick!");
        }
    }
}
```
Но если вы попробуете запустить этот код, то ничего не произойдет. Это потому, что метод `Moroutine.Create` создает морутину и возвращает ее, но не запускает процесс ее выполнения.

### Запуск выполнения морутины
Вы можете запустить ее вызвав метод `Run` как в примере ниже.
```c#
var mor = Moroutine.Create(TickEnumerator());
mor.Run();
```
Пример выше можно сократить используя цепочку вызовов методов.
```c#
Moroutine.Create(TickEnumerator()).Run();
```
Этот пример тоже можно сократить, используя статический метод `Moroutine.Run`.
```c#
Moroutine.Run(TickEnumerator());
```
Используйте метод `Moroutine.Run` если вам надо создать морутину и сразу-же запустить ее. Полный пример кода выглядит так.
```c#
using System.Collections;
using UnityEngine;
using Redcode.Moroutines;

public class Test : MonoBehaviour
{
    private void Start() => Moroutine.Run(TickEnumerator());

    private IEnumerator TickEnumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            print("Tick!");
        }
    }
}
```
> Метод `Moroutine.Run` также возвращает объект морутины, поэтому вы можете использовать его не только для запуска, но и для дальнейших манипуляций.

Если запустить игру с этим скриптом, то в консоли каждую секунду будут появляться сообщения "Tick!".

![Снимок экрана 2021-08-17 211846](https://user-images.githubusercontent.com/5365111/129779572-6c2d7d0d-0c49-4556-918c-5541c0025d13.jpg)

### Остановка морутины
Чтобы остановить морутину используйте метод `Stop` на объекте морутины.
```c#
var mor = Moroutine.Run(TickEnumerator());  // Запускаем

yield return new WaitForSeconds(1f);        // Ждем 1 секунду
mor.Stop();                                 // Останавливаем
```
### Продолжение морутины
Если необходимо продолжить морутину после остановки, то снова вызовите метод Run на ней.
```c#
var mor = Moroutine.Run(TickEnumerator());  // Запускаем

yield return new WaitForSeconds(1f);        // Ждем 1 секунду
mor.Stop();                                 // Останавливаем

yield return new WaitForSeconds(3f);        // Ждем 3 секунды
mor.Run();                                  // Продолжаем
```

### Завершение морутины
Метод (`TickEnumerator()`) который был передан морутине имеет бесконечный цикл внутри. По этой причине такая морутина никогда не завершится. Однако, если передать метод имеющий условие завершения, то морутина рано или поздно будет завершена. Например:
```c#
private void Start() => Moroutine.Run(DelayEnumerator(1f));

private IEnumerator DelayEnumerator(float delay)
{
    yield return new WaitForSeconds(delay);
    print("Completed!");
}
```

В этом случае метод `DelayEnumerator(float delay)` является конечным. Обратите внимание что данный метод генерирует `IEnumerator` объект, что означает, что у этого объекта не будет реализован метод `Reset`, а значит у такого объекта нельзя обнулить состояние. По этой причине, когда морутина, которой был передан `IEnumerator` объект, завершает выполнение, то она автоматически уничтожается (что имеет смысл), что означает, что вы больше не сможете ее запустить снова.

Однако вы можете заменить `IEnumerator` на `IEnumerable` в объявлении метода. `IEnumerable` умеет генерировать объекты `IEnumerator`, что можно использовать как альтернативу методу `Reset`.  
```c#
private void Start() => Moroutine.Run(DelayEnumerable(1f));

private IEnumerable DelayEnumerable(float delay) // Обратите внимание что метод теперь возврашает IEnumerable объект.
{
    yield return new WaitForSeconds(delay);
    print("Completed!");
}
```

В этом случае морутину можно перезапустить, чтобы она начала выполнение с самого начала. Именно по этой причине такие морутины не уничтожаются автоматически.

### Настройка авто-уничтожения
Вы можете управлять автоматическим уничтожением морутины с помощью метода `SetAutoDestroy` или свойства `AutoDestroy`:
```c#
private void Start() => Moroutine.Run(DelayEnumerable(1f)).SetAutoDestroy(true); // <-- настройка авто-уничтожения

private IEnumerable DelayEnumerable(float delay)
{
    yield return new WaitForSeconds(delay);
    print("Completed!");
}
```

В примере выше морутина по умолчанию не уничтожается автоматически, однако с помощью метода `SetAudoDestroy` мы указали, что после завершения ее надо уничтожить. Точно так же вы можете отменить авто-уничтожение морутины созданной с помощью `IEnumerator` объекта, однако это не имеет особого смысла, потому что после завершения такая морутина просто не будет ничего делать, даже если пытаться ее запустить снова и снова.

### Ручное уничтожение морутины
Вы можете уничтожить морутину с помощью вызова ее метода `Destroy`:
```c#
var mor = Moroutine.Run(TickEnumerator());
yield return new WaitForSeconds(3.5f)
mor.Destroy(); // Остановка и уничтожение морутины.
```

> Если морутина более не используется в вашей игре, то она должна быть уничтожена, иначе память не будет освобождена.

### Перезапуск морутины
Вы можете перезапустить морутину (начать ее выполнение с самого начала), для этого используйте метод `Reset`.
```c#
private IEnumerator Start()
{
    var mor = Moroutine.Run(TimerEnumerable());
    yield return new WaitForSeconds(3.5f);    // Ждем 3.5 секунды..

    mor.Reset();    // Останавливаем и обнуляем морутину (возвращаем в исходное состояние).
    mor.Run();      // Запускаем повторно.
}

private IEnumerable TimerEnumerable()
{
    var seconds = 0;

    while (true)
    {
        yield return new WaitForSeconds(1f);
        print(++seconds);
    }
}
```

![image](https://user-images.githubusercontent.com/5365111/161645791-dcf234b7-bc08-480a-b534-da546b1be91f.png)


Обратите внимание, что вызов метода `Reset` обнуляет состояние морутины и останавливает ее. Это значит вы сами должны позаботиться о ее дальнейшем запуске. Методы `Run`, `Stop` и `Reset` возвращают морутину, которой они принадлежат, это позволяет сцепить несколько вызовов методов друг с другом и сократить код.
```c#
mor.Reset().Run();
```

Этот код тоже можно сократить используя методя `Rerun`, который вызывает методы `Reset`и `Run` последовательно.
```c#
mor.Rerun();
```

После выполнения морутины вы также можете вызвать метод `Reset` или `Rerun` на ней, чтобы снова использовать, однако в этом случае это скорее всего будет лишним. Вместо этого просто используйте метод `Run`, он имеет параметр `rerunIfCompleted`, который можно использовать если надо повторно воспроизвести морутину после завершения. По умолчанию этот параметр имеет значение `true`.

### Состояние морутины
Вы можете проверить состояние морутины с помощью следующих свойств:
- `IsReseted` - обнулена ли морутина?
- `IsRunning` - запущена ли морутина?
- `IsStopped` - остановлена ли морутина?
- `IsCompleted` - завершена ли морутина?
- `IsDestroyed` - уничтожена ли морутина?
- `CurrentState` - возвращает перечисление, которое представляет одно из вышеперечисленных состояний.

Первые четыре возвращают булево значение, представляющее соответствующее состояние. Пример:
```c#
var mor = Moroutine.Run(CountEnumerable());
print(mor.IsRunning);
```

### События и методы подписки
Морутины имеют следующие события:
- `Reseted` - срабатывает когда морутина сбрасывается в начальное состояние.
- `Running` - срабатывает сразу после вызова метода `Run`.
- `Stopped` - срабатывает только когда морутина остановилась.
- `Completed` - срабатывает когда морутина завершилась.
- `Destroyed` - срабатывает когда морутина уничтожена.

Вы можете подписаться на любое из этих событий когда это необходимо. Метод-подписчик должен соответствовать следующей сигнатуре:
```c#
void EventHandler(Moroutine moroutine);
```
В параметр `moroutine` будет подставлена морутина вызвавшая событие.
```c#
var mor = Coroutine.Run(CountEnumerable());
mor.Completed += mor => print("Completed");
```
Вы также можете быстро подписаться на нужное событие с помощью следющих методов:
- OnReseted - подписка на обнуление.
- OnRunning - подписка на запуск.
- OnStopped - подписка на остановку.
- OnCompleted - подписка на завершение.
- OnDestroyed - подписка на уничтожение.

```c#
var mor = Moroutine.Run(CountEnumerable());
mor.OnCompleted(c => print("Completed"));
```
Все эти методы возвращают морутину на которой они вызываются, поэтому вы можете формировать длинные цепочки вызовов, например такие:
```c#
Moroutine.Create(CountEnumerable()).OnCompleted(c => print("Completed")).Run();
```

### Ожидание морутины
Если необходимо подождать определенное состояние морутины, то используйте следующие методы:
- `WaitForComplete` - возвращает объект для ожидания завершения.
- `WaitForStop` - возвращает объект для ожидания остановки.
- `WaitForRun` - возвращает объект для ожидания запуска.
- `WaitForReset` - возвращает объект для ожидания обнуления.
- `WaitForDestroy` - возвращает объект для ожидания уничтожения.

Вызовите вышеперечисленные методы для того, чтобы подождать нужное состояние, например:
```c#
var mor = Moroutine.Run(CountEnumerable());

yield return mor.WaitForComplete();             // ждем пока морутина завершится
print("Awaited");                               // выводим текст после завершения морутины
```
Пример выше можно сократить до такого:
```c#
yield return Moroutine.Run(CountEnumerable()).WaitForComplete();
print("Awaited");
```
Во встроенном движке корутин вы были ограничены в количестве ожидающих корутин, то есть одну корутину могла ждать только одна другая корутина, например такой код сообщил бы об ошибке ожидания второй корутиной:
```c#
private void Start()
{
    var coroutine = StartCoroutine(SomeEnumerator());    // первая корутина, имитирует некий процесс
    StartCoroutine(WaitEnumerator(coroutine));           // вторая корутина, ждет первую, все ок
    StartCoroutine(WaitEnumerator(coroutine));           // третья корутина, ждет первую, ошибка
}

private IEnumerator SomeEnumerator()
{
    yield return new WaitForSeconds(3f); // имитируем некий процесс выполнения..
}

private IEnumerator WaitEnumerator(Coroutine coroutine)
{
    yield return coroutine;
    print("Awaited");
}
```

![image](https://user-images.githubusercontent.com/5365111/129798948-97ad275f-1c06-4983-83a2-ab293673347d.png)

Как видите, это действительно так, однако с морутинами такой проблемы нет, вы можете создавать сколько угодно морутин, которые будут ожидать какие угодно другие морутины!
```c#
private void Start()
{
    var mor = Moroutine.Run(SomeEnumerable());
    Moroutine.Run(WaitEnumerable(mor));
    Moroutine.Run(WaitEnumerable(mor));      
}

private IEnumerable SomeEnumerable()
{
    yield return new WaitForSeconds(3f);
}

private IEnumerable WaitEnumerable(Moroutine moroutine)
{
    yield return moroutine.WaitForComplete();
    print("Awaited");
}

```

![image](https://user-images.githubusercontent.com/5365111/129799598-7ebef6dc-a78b-4174-858a-07338e400a3f.png)

### Работа с несколькими морутинами
Вы можете создавать несколько морутин сразу с помощью методов `Create` и `Run`.
```c#
private void Start()
{
    List<Moroutine> mors = Moroutine.Run(TickEnumerable("mor1", 1), TickEnumerable("mor2", 2));
}

private IEnumerable TickEnumerable(string prefix, int count)
{
    for (int i = 0; i < count; i++)
    {
        yield return new WaitForSeconds(1f);
        print($"{prefix}: Tick!");
    }
}
```

![image](https://user-images.githubusercontent.com/5365111/161760852-cc572426-7c66-4ce8-9bc9-590f45bdf18d.png)

В этом случае метод будет возвращать список созданных морутин.

# Ожидание завершения нескольких морутин
Вы также можете ожидать несколько морутин сразу с помощью объекта класса `WaitForAll`.

```c#
private IEnumerator Start()
{
    var mors = Moroutine.Run(TickEnumerable("mor1", 1), TickEnumerable("mor2", 2));
    yield return new WaitForAll(mors);

    print("All awaited!");
}

private IEnumerable TickEnumerable(string prefix, int count)
{
    for (int i = 0; i < count; i++)
    {
        yield return new WaitForSeconds(1f);
        print($"{prefix}: Tick!");
    }
}
```

![image](https://user-images.githubusercontent.com/5365111/161756110-8862133c-5991-42c9-8eb2-5f8e9588dd36.png)

В метод `WaitForAll` также можно передавать `params Moroutine[]`, `IEnumerator[]` или `IEnumerable<IEnumerator>` для ожидания.

# Ожидание завершения хотя бы одной из нескольких морутин
Помимо класса `WaitForAll` есть еще и `WaitForAny`. С помощью него можно подождать выполнение хотя бы одной морутины из указанных.
```c#
private IEnumerator Start()
{
    var tickMor1 = Moroutine.Run(TickEnumerable("mor1", 1));
    var tickMor2 = Moroutine.Run(TickEnumerable("mor2", 2));

    yield return new WaitForAny(tickMor1, tickMor2);
    print("Any awaited!");
}

private IEnumerable TickEnumerable(string prefix, int count)
{
    for (int i = 0; i < count; i++)
    {
        yield return new WaitForSeconds(1f);
        print($"{prefix}: Tick!");
    }
}
```

![image](https://user-images.githubusercontent.com/5365111/161758650-3305b167-cb34-4f0d-b90a-05dffbcf9303.png)

В метод `WaitForAny` также можно передавать `IList<Moroutine>`, `IEnumerator[]` или `IEnumerable<IEnumerator>` для ожидания.

### Результат морутины
Вы также можете без проблем получить последний объект (который вернул `yeild return` оператор) через свойство `LastResult` у морутины.

```c#
private IEnumerator Start()
{
    var mor = Moroutine.Run(_owner, GenerateSomeResultEnumerable());
    yield return mor.WaitForComplete(); // ждем морутину.
    
    print(mor.LastResult); // выводим ее последний результат.
}

private IEnumerable GenerateSomeResultEnumerable()
{
    yield return new WaitForSeconds(3f); // симулируем некий процесс..
    yield return "Hello from moroutine!"; // а это будет последним результатом морутины.
}
```

![image](https://user-images.githubusercontent.com/5365111/141380042-0a009674-c783-4c24-8083-15acb3d6513f.png)

Иногда это бывает очень удобно!

### Бесхозные морутины
До сих пор мы с вами изучали как создавать бесхозные морутины. Бесхозная морутина - это морутина, которая не привязана ни к одному игровому объекту. Выполнение такой морутины не может быть прервано, кроме как с помощью методов `Stop`, `Reset` или `Destroy`. 

### Морутины и их владельцы
Вы можете связать морутину с любым игровым объектом, то есть сделать этот игровой объект владельцем морутины. Это значит, что выполнение морутины будет возможным только в случае если объект-владелец активен, в противном случае морутина будет остановлена и вы не сможете перезапустить ее или продолжить до тех пор пока объект-владелец не станет активен. Попытка запуска морутины на неактивном объекте-владельце сгенерирует исключение. Если объект-владелец вновь активен, то вы можете продолжить выполнение морутины используя метод `Run`.

Чтобы указать хозяина морутины укажите его первым параметром в методах `Moroutine.Create` или `Moroutine.Run`.
```c#
var mor = Moroutine.Run(gameObject, CountEnumerable()); // gameObject - это хозяин морутины
```

Вы также можете вместо хозяина морутины передать любой его компонент. Результат будет таким же.

```c#
var mor = Moroutine.Run(this, CountEnumerable()); // this - это ссылка на текущий компонент по которому будет найден хозяин морутины
```

> Используйте `this` вместо `gameObject`, так как это короче. 
> Вы не можете изменить хозяина морутины после того как морутина была создана.

Если вам необходимо получить хозяина морутины, то вы можете использовать свойство `Owner` у объекта морутины.
```c#
var mor = Moroutine.Run(gameObject, CountEnumerable());
print(mor.Owner.name);
```

> `Owner` - это ссылка на компонент `Owner` владельца морутины.
> У бесхозной морутины `Owner` равен `null`.

### Объект `MoroutinesExecuter`
Перед запуском вашей игры в сцене будет создан объект `MoroutinesExecuter`, который будет изолирован в `DontDestroyOnLoad` сцену и скрыт в ней, так что вы не заметите его. Вы также не сможете получить доступ к этому классу из кода. Данный объект является хозяином всех бесхозных морутин.

### Компонент `Owner`
Любой морутине при ее создании может быть назначен владелец. Владелец - это обычный игровой объект. В момент назначения владельца морутины, к нему добавляется компонент `Owner`, который будет отслеживать деактивацию этого игрового объекта и соответственно останавливать выполнение морутины.

![image](https://user-images.githubusercontent.com/5365111/161753063-5ee9ae8e-83c8-434d-b873-0099988defa9.png)

К одному владельцу могут быть назначены множество морутин. Компонент `Owner` будет существовать до тех пор, пока у него есть хотя бы одна не уничтоженная морутина.
> Не пытайтесь повлиять на компонент `Owner`. В этом нет никакого смысла.

### Получение всех морутин владельца
Вы можете получить все не уничтоженные морутины любого владельца. Для этого подключите пространство имен `Redcode.Moroutines.Extensions` и используйте метод `GetMoroutines` на игровом объекте.
```c#
// ...
using Redcode.Moroutines.Extensions;
// ...

private IEnumerator Start()
{
    Moroutine.Run(this, TickEnumerable(1), TickEnumerable(2));

    var mors = gameObject.GetMoroutines();
    yield return new WaitForAll(mors);

    print("All awaited!");
}

private IEnumerable TickEnumerable(int count)
{
    for (int i = 0; i < count; i++)
    {
        yield return new WaitForSeconds(1f);
        print("Tick!");
    }
}
```

Вы также можете использовать маску состояния чтобы отфильтровать морутины.
```c#
var mors = gameObject.GetMoroutines(Moroutine.State.Stopped | Moroutine.State.Running);
```

### Получение всех бесхозных морутин
Используйте статический метод `Moroutine.GetUnownedMoroutines` для получения бесходных морутин. Также можно использовать маску состояния.

```c#
var mors = Moroutine.GetUnownedMoroutines(Moroutine.State.Running);
```

### Вспомогательный класс `Routines`
Статический класс `Routines` хранит в себе наиболее часто используемые методы для организации логики выполнения морутин. Все методы генерируют и возвращают объект `IEnumerable`, который можно использовать подставляя в другие методы. В частности имеются следующие методы:
- `Delay` - добавляет временную задержку перед выполнением морутины.
- `FrameDelay` - добавляет кадровую задержку перед выполнением морутины.
- `Repeat` - повторяет морутину указанное количество раз.
- `Wait` - ожидает выполнение объектов `YieldInstruction` и `CustomYieldInstruction`.

Пример с `Delay`:
```c#
private void Start() => Moroutine.Run(Routines.Delay(1f, CountEnumerable()));

private IEnumeraable CountEnumerable()
{
    for (int i = 1; i <= 3; i++)
    {
        yield return new WaitForSeconds(1f);
        print(i);
    }
}
```
В этот примере используется метод `Delay`, который добавляет секундную задержку перед выполнением перечислителя `CountEnumerator`, для этого используется строка `Routines.Delay(1f, CountEnumerable())`. Как уже говорилось выше, все методы класса `Routines` возвращают объект `IEnumerable`, поэтому, чтобы сделать из результата склейки методов `Delay` и `CountEnumerable` морутину, нужно подставить его (результат) в метод `Moroutine.Run`.

Метод `Delay` умеет также работать с Action-методами, что по сути дает вам возможность быстро организовать отложенное выполнение нужного вам метода, например:
```c#
private void Start() => Moroutine.Run(Routines.Delay(1f, () => print("Delayed print!")));
```

или
```c#
private void Start() => Moroutine.Run(Routines.Delay(1f, () => Welcome("Andrew", 29)));

private void Welcome(string name, int age) => print($"Hello {name}, you are {age} years old!");
```

![image](https://user-images.githubusercontent.com/5365111/129882932-0ade0009-9599-4226-9567-046fa6a91762.png)

Как видите это очень удобно и сокращает код.
> Данные методы могут работать как с `IEnumerable` так и с `IEnumerator` объектами, однако, если вы планируете перезапуск ваших перечислителей, то необходимо использовать `IEnumerable` объекты.

Метод `FrameDelay` добавляет кадровую задежку перед выполнение перечислителя. К примеру, если нужно подождать 1 игровой кадр, а затем выполнить код перечислителя, то это будет выглядеть так:
```c#
private void Start() => Moroutine.Run(Routines.FrameDelay(1, () => print("1 frame skipped!")));
```
Данный метод также как и метод `Delay` умеет работать с Action-методами.

Метод `Repeat` повторяет указанный перечислитель указанное количество раз. Если вам нужно бесконечное повторение выполнения перечислителя, то укажите -1 в качестве параметра `count` метода `Repeat`. Пример:
```c#
private void Start() => Moroutine.Run(Routines.Repeat(3, WaitAndPrintEnumerator()));

private IEnumerable WaitAndPrintEnumerator()
{
    yield return new WaitForSeconds(1f);
    print("Hello!");
}
```
В результате в консоль 3 раза через каждую секунду будет выведен текст "Hello!".

![image](https://user-images.githubusercontent.com/5365111/129884060-ffa723c8-c7c7-4fe7-8400-e7b3dc37ee40.png)

Вы можете комбинировать методы `Delay`, `FrameDelay` и `Repeat` между собой, например, если нужно выполнить некую функцию 3 раза с задержкой в 1 секунду, то это будет выглядеть так:
```c#
private void Start() => Moroutine.Run(Routines.Repeat(3, Routines.Delay(1f, () => print(Time.time))));
```

![image](https://user-images.githubusercontent.com/5365111/129884562-3ad492a7-de74-466f-be0e-3cbe27654555.png)

Такое вложение методов друг в друга может быть неограниченным.

Метод `Wait` позволяет быстро обернуть `YieldInstrution` или `CustomYieldInstruction` объект в `IEnumerable`, который просто будет ждать их выполнения. Например, если вы хотите обернуть `YieldInstruction` объект в корутину, чтобы в дальнейшем следить за состоянием выполнения `YieldInstruction` через эту корутину, то вы можете написать такой код:
```c#
var moroutine = Moroutine.Run(Routines.Wait(instruction));
```
Где `instruction` это объект класса `YieldInstruction`.

### Расширения
Помимо основного пространства имен существует еще и пространство имен `Moroutines.Extensions`, в котором находятся методы расширения для классов `YieldInstruction` и `CustomYieldInstruction`. Эти методы позволяют быстро конвертировать `Moroutine`, `YieldInstruction` и `CustomYieldInstruction` друг в друга. Например:
```c#
var delayMoroutine = Moroutine.Run(Routines.Delay(1f, () => print("Converting")));  // Создали морутину

var yieldInstruction = delayMoroutine.WaitForComplete();                            // Получили YieldInstruction объект
var customYieldInstruction = yieldInstruction.AsCustomYieldInstruction();           // YieldInstruction конвертировали в CustomYieldInstruction
var moroutine = customYieldInstruction.AsMoroutine();                               // CustomYieldInstruction конвертировали в новый объект Moroutine
```

Такое преобразование скорее всего редко вам понадобится, однако возможность имеется.

Все! Теперь вы готовы использовать наш движок корутин, удачи!
