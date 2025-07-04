﻿using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Payments.ValueObjects;

namespace OnlineStore.Application.Payments.Commands.CreatePayment
{
    public sealed record CreatePaymentCommand(
        Guid OrderId,
        decimal Amount,
        string CurrencyCode,
        DateTime PaymentDate,
        PaymentMethod PaymentMethod) : ICommand<Guid>;
}
