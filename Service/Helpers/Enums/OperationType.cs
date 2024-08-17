namespace Service.Helpers.Enums
{
    public enum OperationType
    {
        Logout,
        CreateEducation,
        UpdateEducation,
        DeleteEducation,
        GetAllEducations,
        GetAllEducationsWithGroups,
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
