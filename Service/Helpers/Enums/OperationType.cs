namespace Service.Helpers.Enums
{
    public enum OperationType
    {
        Exit,
        CreateEducation,
        UpdateEducation,
        DeleteEducation,
        GetAllEducations,
        GetAllEducationsWithGroups,
        GetAllStudentsByGroupId,
        GetEducationById,
        SortEducationsWithCreatedDate,
        SearchEducationsByName,
        CreateGroup,
        UpdateGroup, 
        DeleteGroup,
        GetAllGroups,
        GetAllGroupsWithEducationId,
        GetGroupById,
        FilterGroupsByEducationName,
        SortGroupsWithCapacity,
        SearchGroupsByName,
        
    }
}
