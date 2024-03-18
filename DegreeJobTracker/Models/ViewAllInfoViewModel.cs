namespace DegreeJobTracker.Models {
    public class ViewAllInfoViewModel {

        public ViewAllInfoViewModel(int person_id, string name, List<Job> jobs, List<Degree> degrees) {
            PersonId = person_id;
            Name = name;
            Jobs = jobs;
            Degrees = degrees;
        }

        public int PersonId { get; set; }
        
        public string Name { get; set; }

        public List<Job> Jobs { get; set; }

        public List<Degree> Degrees { get; set; }
    }
}
