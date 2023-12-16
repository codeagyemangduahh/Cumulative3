

using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolApp.Models;

namespace SchoolApp.Controllers;
public class TeacherController : Controller
{
    // The database context class which allows us to access our MySQL Database.
    private SchoolDbContext SchoolDb = new SchoolDbContext();

    [HttpGet]
    public IActionResult List()
    {
        MySqlConnection connection = SchoolDb.AccessDatabase();
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM teachers";
        MySqlDataReader reader = command.ExecuteReader();

        List<Teacher> teachers = new List<Teacher>();

        while (reader.Read())
        {
            Teacher teacher = new Teacher();
            teacher.Id = (int)reader["teacherid"];
            teacher.EmployeeNumber = reader["employeenumber"]?.ToString();
            teacher.HireDate = DateTime.Parse(reader["hiredate"]?.ToString());
            teacher.FirstName = reader["teacherfname"]?.ToString();
            teacher.LastName = reader["teacherlname"].ToString();
            teacher.Salary = decimal.Parse(reader["salary"]?.ToString());

            teachers.Add(teacher);
        }

        connection.Close();

        return View(teachers);
    }

    [HttpGet]
    public IActionResult Show(int teacherid)
    {
        MySqlConnection connection = SchoolDb.AccessDatabase();
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM teachers WHERE teacherid=@id";
        command.Parameters.AddWithValue("@id", teacherid);
        MySqlDataReader reader = command.ExecuteReader();

        Teacher teacher = new Teacher();

        if (reader.Read())
        {
            teacher.Id = (int)reader["teacherid"];
            teacher.EmployeeNumber = reader["employeenumber"]?.ToString();
            teacher.HireDate = DateTime.Parse(reader["hiredate"]?.ToString());
            teacher.FirstName = reader["teacherfname"]?.ToString();
            teacher.LastName = reader["teacherlname"].ToString();
            teacher.Salary = decimal.Parse(reader["salary"]?.ToString());
        }

        connection.Close();

        return View(teacher);
    }

    [HttpGet]
    public IActionResult New()
    {
        Teacher teacher = new Teacher();
        return View(teacher);
    }

    [HttpGet]
    public IActionResult Update(int teacherId)
    {
         MySqlConnection connection = SchoolDb.AccessDatabase();
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM teachers WHERE teacherid=@teacherid";
        command.Parameters.AddWithValue("@teacherid", teacherId);

         MySqlDataReader reader = command.ExecuteReader();

        Teacher teacher = new Teacher();

        if(reader.Read()){
            teacher.Id = (int)reader["teacherid"];
            teacher.EmployeeNumber = reader["employeenumber"].ToString();
            teacher.HireDate = DateTime.Parse(reader["hiredate"].ToString());
            teacher.FirstName = reader["teacherfname"].ToString();
            teacher.LastName = reader["teacherlname"].ToString();
            teacher.Salary = decimal.Parse(reader["salary"].ToString());
        }

        connection.Close();
        return View(teacher);
    }
}