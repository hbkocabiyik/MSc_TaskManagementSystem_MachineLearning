using System;
using System.IO;
using System.Linq;
using TaskManagement.Web.Models.Data;

namespace TaskManagement.DataTransformator
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputFile = new FileInfo("db_management.dat");

            if (outputFile.Exists)
                outputFile.Delete();

            using (var fileStream = outputFile.OpenWrite())
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    using (var dbContext = new TaskManagementContext())
                    {
                        foreach (var task in dbContext.Tasks.Where(t => t.Employee.Login != "slawek"))
                        {
                            var dbManagementRow = new DbManagementRow {
                                                                          Employee = task.Employee.Login,
                                                                          Area = task.Area.Name.GetEnum<AreaName>()
                                                                      };


                            foreach (var taskSkill in task.TaskSkills)
                                dbManagementRow.CheckValue(taskSkill.Skill.Name);

                            streamWriter.WriteLine(dbManagementRow.ToString());
                        }
                    }
                }
            }
        }
    }
}
