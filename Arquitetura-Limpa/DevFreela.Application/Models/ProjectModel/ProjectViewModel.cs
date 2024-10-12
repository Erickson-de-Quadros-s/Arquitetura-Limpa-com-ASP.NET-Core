using DevFreela.Core.Entities;

namespace DevFreela.Application.Models.ProjetctModel
{
    public class ProjectViewModel
    {
        public ProjectViewModel(string title, string description, int idClient, string clientName, int idFreelancer, string freelancerName, decimal totalCost, List<ProjectComment> comments)
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            ClientName = clientName;
            IdFreelancer = idFreelancer;
            FreelancerName = freelancerName;
            TotalCost = totalCost;

            Comments = comments.Select(c => c.Content).ToList();
        }


        public string Title { get; private set; }
        public string Description { get; private set; }
        public int IdClient { get; private set; }
        public string ClientName { get; private set; }
        public int IdFreelancer { get; private set; }
        public string FreelancerName { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<string> Comments { get; private set; }

        public static ProjectViewModel FromEntity(Project entity)
            => new ProjectViewModel(entity.Title, entity.Description, entity.IdClient, entity.Client.Name, entity.IdFreelancer, entity.Freelancer.Name, entity.TotalCost, entity.Comments);

    }
}