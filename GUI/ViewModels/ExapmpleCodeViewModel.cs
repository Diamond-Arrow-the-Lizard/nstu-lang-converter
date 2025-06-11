using CommunityToolkit.Mvvm.ComponentModel;

namespace GUI.ViewModels;

public partial class ExampleCodeViewModel : ViewModelBase
{

    [ObservableProperty]
    private string _ExampleCode = @"
начало
    цел Num1 = 5;
    плав DecVal = 10.5;
    строка Message = ""Привет, мир!"";

    написать ""Начальные значения:"";
    написать Num1;
    написать DecVal;
    написать Message;

    Num1 = Num1 + 2 * 3;
    DecVal = DecVal / 2.0 - 1.5;
    написать ""Новые значения:"";
    написать Num1;
    написать DecVal;

    прочитать Message;
    написать ""Вы ввели: "";
    написать Message;

    цел Var;
    прочитать Var;
    написать ""Var: "";
    написать Var;

    если Num1 == 11 то
        написать ""Num1 стало 11!"";
    иначе
        написать ""Num1 не 11."";
    кесли

    если Num1 <= Var то
        написать ""Num1 меньше или равно Var"";
    иначе
        написать ""Num1 больше Var"";
    кесли

    нц 2 раз
        написать ""Повтор внутри цикла!"";
        цел Counter = 1;
        Counter = Counter + 1;
    кц

    цел x = 10;
    пока x == 10 нц
        написать ""X равно 10"";
        x = 5;
    кц

    цел y = 0;
    нц
        написать ""Y равно"";
        написать y;
        y = y + 1;
    пока y < 3 кц

конец".Trim();

}