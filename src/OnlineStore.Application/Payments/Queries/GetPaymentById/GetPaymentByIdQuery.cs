﻿using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Payments.DTOs;

namespace OnlineStore.Application.Payments.Queries.GetPaymentById
{
    public sealed record GetPaymentByIdQuery(Guid PaymentId) : IQuery<PaymentDto>;
}
