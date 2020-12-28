using System;
using System.Collections.Generic;

namespace Tax0r_Comboleecher.Classes
{
    class FilterHelperClass
    {

        List<string> filters = new List<string>();

        public void AddFilter(string item)
        {
            filters.Add(item);
        }

        public void RemoveFilter(string item)
        {
            filters.Remove(item);
        }

        public List<string> getFilters()
        {
            return filters;
        }

        public Boolean Filtered(string url)
        {
            foreach (string item in filters)
            {
                if (url.Contains(item))
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;
        }
    }
}
