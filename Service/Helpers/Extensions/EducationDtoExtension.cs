using ConsoleTables;
using Service.DTOs.Educations;

namespace Service.Helpers.Extensions
{
    public static class EducationDtoExtension
    {
        public static void PrintAllWithId(this List<EducationDTo> educations)
        {
            var table = new ConsoleTable("Id", "Name", "Color");

            foreach (var item in educations)
            {
                table.AddRow(item.Id, item.Name, item.Color);
            }

            table.Options.EnableCount = false;

            table.Write();
        }
    }
}
