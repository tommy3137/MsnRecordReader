using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSNrecordReader.Models
{
    public class MsnParseModel
    {
       
            public Log LOG { get; set; }

            public class Log
            {
                public List<Message> MESSAGE { get; set; }

                public class Message
                {
                    public string DATE { get; set; }
                    public string TIME { get; set; }
                    public From FROM { get; set; }
                    public Text TEXT { get; set; }

                    public class From
                    {
                        public User USER { get; set; }
                    }


                    public class User
                    {
                        public string FriendlyName { get; set; }
                    }

                    public class Text
                    {
                        public string text { get; set; }
                    }
                }
            }
        
    }
}
