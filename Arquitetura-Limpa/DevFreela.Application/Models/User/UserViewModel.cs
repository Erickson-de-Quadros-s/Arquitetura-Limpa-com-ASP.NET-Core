using DevFreela.Core.Entities;

namespace DevFreela.Application.Models.Users
{
    public class UserViewModel
    {
        public UserViewModel(string name, string email, DateTime birthDate, List<string> skills)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Skills = skills;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate
        {
            get; set;
        }
        public List<string> Skills { get; private set; }

        public static UserViewModel FromEntity(User user)
        {
            var skills = user.Skills.Select(u => u.Skill.Description).ToList();

            return new UserViewModel(user.Name, user.Email, user.BirthDate, skills);
        }
    }
}