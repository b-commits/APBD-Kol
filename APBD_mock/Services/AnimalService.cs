using APBD_mock.DTOs;
using APBD_mock.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_mock.Services
{
    public class AnimalService : IAnimalsDbService
    {
        private const string ConnectionString = "Data Source=db-mssql;Initial Catalog=s19677;Integrated Security=true";
        public List<AnimalsResponse> GetAnimals(string sortBy, string ascDesc)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                command.Parameters.AddWithValue("sortBy", sortBy);
                command.Parameters.AddWithValue("asc", ascDesc);
                command.CommandText = "SELECT * FROM Animal ORDER BY name asc";
                SqlDataReader reader = command.ExecuteReader();
                List<AnimalsResponse> animalResponse = new List<AnimalsResponse>();
                while (reader.Read())
                {
                    AnimalsResponse response = new AnimalsResponse();
                    response.Name = reader[1].ToString();
                    response.AnimalType = reader[2].ToString();
                    response.DateOfAdmission = DateTime.Parse(reader[3].ToString());
                    response.LastNameOfOwner = reader[4].ToString();
                    animalResponse.Add(response);
                }
                return animalResponse;
            }
        }

        public Animal InsertAnimal(AnimalRequest animal)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand())
            {
                {
                    connection.Open();
                    SqlTransaction transcation = connection.BeginTransaction();
                    command.Transaction = transcation;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("name", animal.Name);
                    command.Parameters.AddWithValue("type", animal.AnimalType);
                    command.Parameters.AddWithValue("admissionDate", animal.AdmissionDate);
                    command.Parameters.AddWithValue("idOwner", animal.IdOwner);
                    command.CommandText = "INSERT INTO Animal VALUES (@name, @type, @admissionDate, @idOwner)";
                    command.ExecuteScalar();
                    
                    command.CommandText = "SELECT IdAnimal FROM Animal WHERE name = @name AND idOwner = @idOwner";
                    SqlDataReader dr = command.ExecuteReader();
                    dr.Read();
                    int id = dr.GetInt32(0);
                    dr.Close();

                    foreach (Procedure p in animal.UnderwentProcedure) 
                    {
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("idProcedure", p.IdProcedure);
                        command.Parameters.AddWithValue("procedureDate", p.ProcedureDate);
                        command.CommandText = "INSERT INTO Procedure_Animal VALUES (@idProcedure, @id, @procedureDate)";
                        command.ExecuteScalar();
                        command.Parameters.Clear();
                
                    }

                    transcation.Commit();

                    Animal inserted = new Animal
                    {
                        Name = animal.Name,
                        IdAnimal = animal.IdAnimal,
                        IdOwner = animal.IdOwner,
                        Type = animal.AnimalType,
                        AdmissionDate = animal.AdmissionDate
                    };
                    return inserted;
                }
            }
        }
    }
}
