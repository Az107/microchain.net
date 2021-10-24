using System;
namespace Microchain{
    public class Block
        {
            public int id { get; private set; }
            public String hash { get; private set; }
            public Object value { get; private set; }

            public Block(int id, String hash, Object value)
            {
                this.id = id;
                this.hash = hash;
                this.value = value;
            }
        }
}