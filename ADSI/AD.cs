using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;

namespace ADSI
{
    static class AD
    {
        public static SearchResultCollection queryAD(string filtro, SearchScope scope)
        {
            //ArrayList lista = EnumerateOU("cn=Users,dc=me,dc=inet");

            //CN=System,DC=tcn,DC=com,DC=ni
            DirectoryEntry de = new DirectoryEntry();
            SearchResultCollection result = null;
            using (DirectorySearcher searcher = new DirectorySearcher())
            {
                searcher.SearchRoot = de;
                searcher.SearchScope = scope;
                searcher.Filter = filtro;
                
                result = searcher.FindAll();

            }
            return result;
        }
        public static SearchResultCollection queryAD(string filtro, string root, SearchScope scope,string[] properties)
        {
            //ArrayList lista = EnumerateOU("cn=Users,dc=me,dc=inet");

            //CN=System,DC=tcn,DC=com,DC=ni
            DirectoryEntry de = new DirectoryEntry("LDAP://"+root);
            SearchResultCollection result = null;
            //IEnumerable<SearchResult> sr = null;
            using (DirectorySearcher searcher = new DirectorySearcher())
            {
                searcher.SearchRoot = de;
                searcher.SearchScope = scope;
                searcher.Filter = filtro;
                foreach(string prop in properties)
                {
                    searcher.PropertiesToLoad.Add(prop);
                }
                searcher.PageSize = 1000;
                result = searcher.FindAll();
                Console.WriteLine("Resultados: "+result.Count);
                //sr = SafeFindAll(searcher);
            }
            return result;
        }

        private static IEnumerable<SearchResult> SafeFindAll(DirectorySearcher searcher)
        {
            using (SearchResultCollection results = searcher.FindAll())
            {
                foreach (SearchResult result in results)
                {
                    yield return result;
                }
            } // SearchResultCollection will be disposed here
        }

        public static List<String> ADScopes()
        {
            List<String> lista = new List<String>();

            SearchResultCollection results = queryAD("(objectClass=trustedDomain)", SearchScope.Subtree);

            foreach (SearchResult sr in results) {
                lista.Add(sr.Properties["cn"][0].ToString());
            }
            lista.Add(Domain.GetCurrentDomain().ToString());

            return lista;
        }

        public static List<String> domainControllers()
        {
            List<String> alDcs = new List<String>();
            Domain domain = Domain.GetCurrentDomain();
            //Console.WriteLine(domain.ToString());
            foreach (DomainController dc in domain.DomainControllers)
            {
                alDcs.Add(dc.Name);
            }
            
            alDcs.Sort();
            return alDcs;

        }


    }
}
