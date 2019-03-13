using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2019
{
    class Slide
    {
        public List<int> ids;
        public List<string> tags;
        

        public Slide(Photo photo)
        {
            ids = new List<int>();
            ids.Add(photo.id);
            tags = photo.tags;
        }

        public Slide(Photo photo1, Photo photo2)
        {
            ids = new List<int>();
            ids.Add(photo1.id);
            ids.Add(photo2.id);

            tags = photo1.tags;

            for (int i = 0; i < photo2.tags.Count; i++)
            {
                if (!tags.Contains(photo2.tags[i]))
                {
                    tags.Add(photo2.tags[i]);
                }
            }
        }
    }
}
