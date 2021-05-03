using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModuloGameServer.Models
{

    public class BaseDataSource
    {
        protected ModuloGameDBContext context;

        protected  delegate void TaskDelegate();

        public BaseDataSource(ModuloGameDBContext context)
        {
            this.context = context;
        }

        protected async Task InTransaction( Func<Task<bool>> requestTask, CancellationToken cancellationToken)
        {
            bool isNoError = false;
            isNoError = true;
            // TODO вспомнить про уровни изоляции и выбрать подходящий
            var transaction = await context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable, cancellationToken);
            try
            {
                isNoError = await requestTask.Invoke();
            }
            finally
            {
                if (isNoError)
                {
                    await transaction.CommitAsync();
                }
                else
                {
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}
