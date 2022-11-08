﻿namespace TaskBoardApp.Services
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Entities;
    using Models;
    using Services.Contracts;

    public class BoardsService : IBoardsService
    {
        private readonly TaskBoardDbContext context;

        public BoardsService(TaskBoardDbContext dbcontext)
            => this.context = dbcontext;

        public async System.Threading.Tasks.Task CreateNewBoard(string boardName)
        {
            Board board = new Board()
            {
                Id = Guid.NewGuid(),
                Name = boardName,
            };

            this.context.Boards.Add(board);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<BoardViewModel>> GetAllBoards()
           => await this.context.Boards.Select(b => 
                                    new BoardViewModel()
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                    })
                                .OrderBy(b => b.Name)
                                .ToListAsync();

        public async Task<List<string>> GetAllBoardsNames()
            => await this.context.Boards.Select(b => b.Name).OrderBy(b => b).ToListAsync();
    }
}