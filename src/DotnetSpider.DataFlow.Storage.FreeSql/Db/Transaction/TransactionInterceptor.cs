using Castle.DynamicProxy;

namespace DotnetSpider.DataFlow.Storage.FreeSql.Db.Transaction;

/// <summary>
/// 事务拦截器
/// </summary>
public class TransactionInterceptor : IInterceptor
{
    private readonly TransactionAsyncInterceptor _transactionAsyncInterceptor;

    public TransactionInterceptor(TransactionAsyncInterceptor transactionAsyncInterceptor)
    {
        _transactionAsyncInterceptor = transactionAsyncInterceptor;
    }

    public void Intercept(IInvocation invocation)
    {
        _transactionAsyncInterceptor.ToInterceptor().Intercept(invocation);
    }
}