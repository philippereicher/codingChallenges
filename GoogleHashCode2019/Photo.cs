using System.Collections.Generic;

namespace HashCode2019
{
    class Photo
    {
        public int id;
        public List<string> tags;
        public bool isHorizontal;

        public Photo(int id, List<string> tags, bool isHorizontal)
        {
            this.id = id;
            this.tags = tags;
            this.isHorizontal = isHorizontal;
        }
    }
}
