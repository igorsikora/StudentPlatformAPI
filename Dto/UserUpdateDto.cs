namespace StudentPlatformAPI.Dto
{
    public class UserUpdateDto : UserDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

    }
    
}