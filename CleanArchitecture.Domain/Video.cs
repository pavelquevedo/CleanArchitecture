﻿namespace CleanArchitecture.Domain
{
    public class Video
    {
        public Video()
        {
            Actors = new HashSet<Actor>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int StreamerId { get; set; }
        public virtual Streamer Streamer { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        public virtual Director Director { get; set; } 
    }
}
