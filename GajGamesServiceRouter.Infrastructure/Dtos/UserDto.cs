using System;
namespace GajGamesServiceRouter.Infrastructure.Dtos
{        
    public class UserDto
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public string UserImgB64 { get; set; }
    }
    
}
