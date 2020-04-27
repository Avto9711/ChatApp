using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using ChatApp.Core.BaseModel.BaseEntity;
using ChatApp.Model.Entities;
using ChatApp.Model.Services;

namespace ChatApp.Model.Contexts.ChatApp
{
    public class ChatAppDbContext : BaseDbContext, IChatAppDbContext
    {
        public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options) : base(options)
        {
        }
        
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomMessage> ChatRoomMessages { get; set; }

        public DbSet<T> GetDbSet<T>() where T : class => Set<T>();

        public override EntityEntry<T> Entry<T>(T entity) 
        {
            return base.Entry(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            
        modelBuilder.Entity<ChatRoom>().HasData(
            new ChatRoom { Id = 1,ChatRoomName="Room Chat 1",ChatRoomCode = Guid.NewGuid().ToString() },
            new ChatRoom { Id = 2,ChatRoomName="Room Chat 2",ChatRoomCode = Guid.NewGuid().ToString() },
            new ChatRoom { Id = 3,ChatRoomName="Room Chat 3",ChatRoomCode = Guid.NewGuid().ToString() }
        );


            base.OnModelCreating(modelBuilder);
        }

    }
}
