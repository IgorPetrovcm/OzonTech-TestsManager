# OzonTestsManager
Данная библиотека предоставляет быстрое тестирование для вашего решения задачи с контекста **OzonTech**.

**Ссылка на nuget-пакет: https://www.nuget.org/packages/IgorPetrovcm.OzonTestsManager**

## Использование 
+ Для работы с **OzonTestsManager** предстоит скачать набор тестов со страницы задачи контекста в виде **.zip** файла.

+ Далее советую создать в директории вашего решения папку **Task** и поместить туда из **.zip**, файл с задачей и с правильным решением. Путь из корня проекта к файлам выглядит так:
    ```
    Путь к файлу с задачей:
    Task/task.txt

    Путь к правильному решению задачи:
    Task/result.txt
    ``` 

+ В коде решения подключим библиотеку:
  ```csharp
  using OzonTestsManager;
  ```
  Создадим экземпляр класса `OzonCurrentTest` таким способом:
  ```csharp
    string[] taskLines = File.ReadAllLines("Task/task.txt");

    string[] resultLines = File.ReadAllLines("Task/result.txt");

    OzonCurrentTest ozonTest = OzonTools.CompleteCreation(taskLines, resultLines);
  ``` 

+ Предположим, что файл с задачей:
  ```txt
    1000
    31 9 2162
    31 8 2118
    31 6 2178
    31 11 2164
    31 2 2233
    31 9 2103
    31 6 2061
    31 10 2243
    31 2 2062
  ``` 
  Файл с решением:
  ```txt
    NO
    YES
    NO
    NO
    NO
    NO
    NO
    YES
    NO
  ```

+ `OzonCurrentTest` прочитает файл с задачей и сам задаст количество тестов, прочитав первую строку файла. Тогда мы можем решать задачу перебирая каждый тест таким образом:
    ```csharp
        string[] tests = ozonTest.ArrayTests;

        for (int i = 0; i < ozonTest.TestsCount/*Или - tests.Length*/; i++) {}
    ```
+ Перед самим перебором тестов и решением их, создадим объект класса `YourTaskResult`, далее мы будем помещать в него результаты нашего решения:
  ```csharp
  YourTaskResult result = new YourTaskResult();
  ``` 
  Допустим при решении теста мы получили результат в виде строки(нужно привести результат к типу данных **строки**), добавим его к нашему результату:
  ```csharp
  //currentTestResult = "yes"

  result.Add(currentTestResult);
  ```

+ После решения всех тестов, выполним следующие:
  ```csharp
  Console.WriteLine(ozonTest.TestChecking(result));
  ```
  И получим результаты нашего решения в консоль!


## Дополнительно

Контакты:

Telegram: @IGOR_PETR
