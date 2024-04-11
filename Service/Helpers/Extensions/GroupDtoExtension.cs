using ConsoleTables;
using Service.DTOs.Groups;

namespace Service.Helpers.Extensions
{
    public static class GroupDtoExtension
    {
        public static void PrintAllWithId(this List<GroupDTo> groups)
        {
            var table = new ConsoleTable("Id", "Name", "Capacity", "EducationId");

            foreach (var item in groups)
            {
                table.AddRow(item.Id, item.Name, item.Capacity, item.EducationId);
            }

            table.Options.EnableCount = false;

            table.Write();
        }
    }
}
