using System;

namespace Application.DataTransferObjects
{
    public class CharacterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string StateOfOrigin { get; set; }
        public string Gender { get; set; }
        public DateTime Created { get; set; }
        public string LocationName { get; set; }
    }
    public class Character
    {
        public int CharacterId { get; set; }
    }
}