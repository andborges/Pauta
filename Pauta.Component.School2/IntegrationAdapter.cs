using MobileItFramework.Security;
using Pauta.Component.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pauta.Component.School2
{
    public class IntegrationAdapter : IIntegrationAdapter
    {
        private static readonly Class[] Classes = 
        {
            new Class
            {
                        Id = "2B_Matematica",
                        ClassRoom = "2B",
                        Discipline = "Matemática (Escola 2)",
                        InitialDate = DateTime.Now.AddHours(-3).AddMinutes(50),
                        EndDate = DateTime.Now.AddHours(-3).AddMinutes(95),
                        Attendances = new List<Attendance>
                                            {
                                                new Attendance { StudentId = "1001", StudentNumber = "2", StudentName = "Marcelo Silva", Attendant = true },
                                                new Attendance { StudentId = "1002", StudentNumber = "3", StudentName = "Tânia Braga Mendes Carvalho Pessoa e Souza" },
                                                new Attendance { StudentId = "1000", StudentNumber = "1", StudentName = "Anderson Saraiva" },
                                            }
            },
            new Class
            {
                        Id = "3F_Portugues",
                        ClassRoom = "3F",
                        Discipline = "Português (Escola 2)",
                        InitialDate = DateTime.Now.AddHours(-3),
                        EndDate = DateTime.Now.AddHours(-3).AddMinutes(45),
                        Attendances = new List<Attendance>
                                            {
                                                new Attendance { StudentId = "1004", StudentNumber = "2", StudentName = "Paulo Moura" },
                                                new Attendance { StudentId = "1005", StudentNumber = "3", StudentName = "Zenon Fontes" },
                                                new Attendance { StudentId = "1003", StudentNumber = "1", StudentName = "Alex Correa" },
                                            }
            }
        };

        public Guid ComponentId
        {
            get { return new Guid("2DB86E37-B653-4823-8BDA-8DE5C737C82F"); }
        }

        public string ComponentName
        {
            get { return "School2"; }
        }

        public ICollection<Class> GetAllClasses(Token token, DateTime date)
        {
            // Optimizing to not return class Attendances in json
            return Classes.Select(c => new Class { Id = c.Id, ClassRoom = c.ClassRoom, Discipline = c.Discipline, InitialDate = c.InitialDate, EndDate = c.EndDate }).OrderBy(c => c.InitialDate).ToList();
        }

        public Class GetClass(Token token, string id)
        {
            // Projecting to order by StudentNumber
            return Classes.Select(c => new Class { Id = c.Id, ClassRoom = c.ClassRoom, Discipline = c.Discipline, InitialDate = c.InitialDate, EndDate = c.EndDate, Attendances = c.Attendances.OrderBy(a => a.StudentNumber).ToList() }).FirstOrDefault(c => c.Id == id);
        }

        public bool SaveClass(Token token, Class myclass)
        {
            var persistedClass = Classes.First(c => c.Id == myclass.Id);

            foreach (var attendance in persistedClass.Attendances)
            {
                attendance.Attendant = myclass.Attendances.First(a => a.StudentNumber == attendance.StudentNumber).Attendant;
            }

            return true;
        }
    }
}