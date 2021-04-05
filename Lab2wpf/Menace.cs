using System;
using System.Collections.Generic;

namespace Lab2wpf
{
    public class Menace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string ObjectOfInfluence { get; set; }
        public bool PrivacyViolation { get; set; }
        public bool IntegrityViolation { get; set; }
        public bool AvailabilityViolation { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime DateOfChange { get; set; }
        public bool ContainId(List<Menace> a)
        {
            foreach (var ir in a)
            {
                if (ir.Id == Id) return true;
            }
            return false;
        }

    }
}
