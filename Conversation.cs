using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectMe
{
    public class Conversation
    {
        public int id;
        public string name;
        public Profile[] members;
        public Message[] messages;
        public bool isStub;
        public Conversation(bool isStub)
        {
            this.isStub = isStub;
        }
        public Conversation()
        {
            this.isStub = false;
        }
    }
}