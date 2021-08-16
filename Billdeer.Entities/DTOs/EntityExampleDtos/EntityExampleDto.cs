using Billdeer.Entities.DTOs.ForeignExampleDtos;
using System;
using System.Collections.Generic;

namespace Billdeer.Entities.DTOs.EntityExampleDtos
{
    public class EntityExampleDto // Get işlemi için kullanılacak, normalde bunda da IDto olacak ama yarın gündüz gösteririm onu.
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ForeignExampleDto> ForeignExamples { get; set; } // Burda da tip olarak Dto karşılığını belirtiyoruz.

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
