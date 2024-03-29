﻿using ComicsStore.Data.Common;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class ArtistOnlyOutputModel : BasicOutputModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public YesNoInd Pseudonym { get; set; }
        public string RealLastName { get; set; }
        public string RealFirstName { get; set; }
    }
}
