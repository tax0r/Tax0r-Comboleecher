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

        public Boolean isSimiliar(string str1, string str2)
        {
            if(str1.Contains(str2) && str1.Length >= str2.Length || str1.Contains(str2) && str1.Length <= str2.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public Boolean failedBefore(string url, string[] fails) 
        {
            foreach (string fail in fails)
            {
                if (isSimiliar(url, fail))
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

        public Boolean Filtered(string url, List<string> fails)
        {
            foreach (string item in filters)
            {
                if (url.Contains(item) || failedBefore(url, fails.ToArray()))
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
