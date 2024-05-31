using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goverment.Core.Persistance.Repositories
{
	public class Entity
	{
		public int Id { get; set; }
		
        public Entity()
        {
            
        }
        public Entity(int id)
        {
			Id = id;
        }
    }
}
