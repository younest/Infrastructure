using System.Configuration;

namespace Interface.Infrastructure.Entities
{
    public sealed class InterfaceMailEntity : ConfigurationSection
    {
        [ConfigurationProperty("enable")]
        public string enable { get { return (string)base["enable"]; } }

        [ConfigurationProperty("smtp")]
        public Smtp smtp { get { return (Smtp)base["smtp"]; } }

        [ConfigurationProperty("outbox")]
        public OutBox outbox { get { return (OutBox)base["outbox"]; } }

        [ConfigurationProperty("inbox")]
        public Inbox inbox { get { return (Inbox)base["inbox"]; } }
    }

    public sealed class Smtp : ConfigurationElement
    {
        [ConfigurationProperty("host")]
        public string Host
        {
            get { return this["host"].ToString(); }
            set { this["host"] = value; }
        }

        [ConfigurationProperty("port")]
        public string Port
        {
            get { return this["port"].ToString(); }
            set { this["port"] = value; }
        }

        [ConfigurationProperty("ssl")]
        public string Ssl
        {
            get { return this["ssl"].ToString(); }
            set { this["ssl"] = value; }
        }

        [ConfigurationProperty("Credentials")]
        public Credentials Credentials
        {
            get { return (Credentials)this["Credentials"]; }
            set { this["Credentials"] = value; }
        }
    }

    public sealed class Credentials : ConfigurationElement
    {
        [ConfigurationProperty("address")]
        public string Address
        {
            get { return this["address"].ToString(); }
            set { this["address"] = value; }
        }

        [ConfigurationProperty("pwd")]
        public string Password
        {
            get { return this["pwd"].ToString(); }
            set { this["pwd"] = value; }
        }
    }

    public sealed class OutBox : ConfigurationElement
    {
        [ConfigurationProperty("subject")]
        public string Subject
        {
            get { return this["subject"].ToString(); }
            set { this["subject"] = value; }
        }

        [ConfigurationProperty("url")]
        public string Url
        {
            get { return this["url"].ToString(); }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("signature")]
        public string Signature
        {
            get { return this["signature"].ToString(); }
            set { this["signature"] = value; }
        }

        [ConfigurationProperty("attachmentPath")]
        public string AttachmentPath
        {
            get { return this["attachmentPath"].ToString(); }
            set { this["attachmentPath"] = value; }
        }
    }

    [ConfigurationCollection(typeof(Group), AddItemName = "group")]
    public sealed class Inbox : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Group();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Group)element).Code;
        }

        public Group this[int index]
        {
            get { return (Group)base.BaseGet(index); }
        }

        new public Group this[string id]
        {
            get { return (Group)base.BaseGet(id); }
        }
    }

    public sealed class Group : ConfigurationElement
    {
        [ConfigurationProperty("code")]
        public string Code
        {
            get { return this["code"].ToString(); }
            set { this["code"] = value; }
        }

        [ConfigurationProperty("name")]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("email")]

        public EmailCollection Emails
        {
            get { return (EmailCollection)this["email"]; }
            set { this["email"] = value; }
        }
    }

    [ConfigurationCollection(typeof(EmailCollection), AddItemName = "add")]
    public sealed class EmailCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Email();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Email)element).Address;
        }

        public Email this[int index]
        {
            get { return (Email)base.BaseGet(index); }
        }

        new public Email this[string address]
        {
            get { return (Email)base.BaseGet(address); }
        }
    }

    public sealed class Email : ConfigurationElement
    {
        [ConfigurationProperty("address")]
        public string Address
        {
            get { return this["address"].ToString(); }
            set { this["address"] = value; }
        }

        [ConfigurationProperty("type")]
        public string Type
        {
            get { return this["type"].ToString(); }
            set { this["type"] = value; }
        }
    }
}
