
using ADO_EmployeePayroll;

EmployeePayroll select = new EmployeePayroll();
ModelClass modelClass = new ModelClass();

Console.WriteLine("1 - Establish Connectivity");
Console.WriteLine("2 - Retrieve or Add Data to DataBase");
Console.WriteLine("3 - Retrieve or Add Data to DataBase");
Console.WriteLine("4 - Update Salary performed");

int option = Convert.ToInt32(Console.ReadLine());
switch (option)
{
    case 1:
        select.DatabseConnection();
        break;
    case 2:
        select.GetAllEmployees();
        break;
    case 3:
        modelClass.name = "Leenu";
        modelClass.basic_pay = 850000;
        modelClass.start = DateTime.Now;
        modelClass.gender = "M";
        select.AddEmployee(modelClass);

        break;
    case 4:
        select.UpdateValue();
        break;
}