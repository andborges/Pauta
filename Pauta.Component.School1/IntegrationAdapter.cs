using MobileItFramework.Security;
using Pauta.Component.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pauta.Component.School1
{
    public class IntegrationAdapter : IIntegrationAdapter
    {
        private static readonly Class[] Classes =
        {
            new Class
                {
                    Id = "2B_Math",
                    ClassRoom = "2B",
                    Discipline = "Matemática (Escola 1)",
                    InitialDate = DateTime.Now.AddHours(-3).AddMinutes(50),
                    EndDate = DateTime.Now.AddHours(-3).AddMinutes(95),
                    Attendances = new List<Attendance>
                                    {
                                        new Attendance { StudentId = "1001", StudentNumber = "3", StudentName = "Marcos Santos", Attendant = true },
                                        new Attendance { StudentId = "1002", StudentNumber = "4", StudentName = "Tânia Braga Mendes Carvalho Pessoa e Souza" },
                                        new Attendance { StudentId = "1000", StudentNumber = "1", StudentName = "Alberto Silva" },
                                        new Attendance { StudentId = "1003", StudentNumber = "2", StudentName = "Júlia Raimundo Medeiros" }
                                    }
                },
            new Class
                {
                    Id = "3F_English",
                    ClassRoom = "3F",
                    Discipline = "Física (Escola 1)",
                    InitialDate = DateTime.Now.AddHours(-3),
                    EndDate = DateTime.Now.AddHours(-3).AddMinutes(45),
                    Attendances = new List<Attendance>
                                    {
                                        new Attendance { StudentId = "1004", StudentNumber = "10", StudentName = "Maria Saraiva" },
                                        new Attendance { StudentId = "1005", StudentNumber = "12", StudentName = "Sérgio Fagundes" },
                                        new Attendance { StudentId = "1006", StudentNumber = "2", StudentName = "André Serra Mendes" },
                                        new Attendance { StudentId = "1007", StudentNumber = "8", StudentName = "Júlia Raimundo Medeiros" },
                                        new Attendance { StudentId = "1008", StudentNumber = "3", StudentName = "Carlos Alberto Paiva" },
                                        new Attendance { StudentId = "1009", StudentNumber = "5", StudentName = "João Santos" },
                                        new Attendance { StudentId = "1010", StudentNumber = "4", StudentName = "Clara Regina Matos" },
                                        new Attendance { StudentId = "1011", StudentNumber = "9", StudentName = "Luis Pereira da Silva" },
                                        new Attendance { StudentId = "1012", StudentNumber = "7", StudentName = "José Gomes Peres" },
                                        new Attendance { StudentId = "1013", StudentNumber = "6", StudentName = "Jorge Borges Menezes" },
                                        new Attendance { StudentId = "1014", StudentNumber = "1", StudentName = "Ana Célia Passos" },
                                        new Attendance { StudentId = "1015", StudentNumber = "11", StudentName = "Paulo Cezar Faro Júnior" }
                                    }
                }
        };

        public Guid ComponentId
        {
            get { return new Guid("A20AE720-8B95-4539-9F34-5AFCB9FDFEF3"); }
        }
        
        public string ComponentName
        {
            get { return "School1"; }
        }

        public ICollection<Class> GetAllClasses(Token token, DateTime date)
        {
            // Optimizing to not return class Attendances in json
            return Classes.Select(c => new Class { Id = c.Id, ClassRoom = c.ClassRoom, Discipline = c.Discipline, InitialDate = c.InitialDate, EndDate = c.EndDate }).OrderBy(c => c.InitialDate).ToList();
        }

        public Class GetClass(Token token, string id)
        {
            // Projecting to order by StudentNumber
            return Classes.Select(c => new Class { Id = c.Id, ClassRoom = c.ClassRoom, Discipline = c.Discipline, InitialDate = c.InitialDate, EndDate = c.EndDate, Attendances = c.Attendances.OrderBy(a => a.StudentName).ToList() }).FirstOrDefault(c => c.Id == id);
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