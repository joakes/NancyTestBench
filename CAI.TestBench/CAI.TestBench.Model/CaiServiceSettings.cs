namespace CAI.TestBench.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable]
    public class CaiServiceSettings 
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

        public DateTime LastUpdated { get; set; }
    }
}
