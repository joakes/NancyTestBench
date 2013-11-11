namespace CAI.TestBench.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CaiServiceSettings : IDataEntity
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ServiceId { get; set; }

        [Required]
        public string Organisation { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [Range(1, short.MaxValue)]
        public short BranchNumber { get; set; }

        public DateTime? LastUpdated { get; set; }
        
        public int Id { get; set; }
        
        public bool AreDefault { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var settings = obj as CaiServiceSettings;
            if (settings == null)
            {
                return false;
            }

            return Equals(settings);
        }

        private bool Equals(CaiServiceSettings other)
        {
            return (ServiceId == other.ServiceId) &&
                   (Organisation == other.Organisation) &&
                   (BranchNumber == other.BranchNumber) &&
                   (Username == other.Username);
        }
    }
}
