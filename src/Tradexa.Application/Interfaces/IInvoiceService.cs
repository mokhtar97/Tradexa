using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradexa.Application.DTOs;

namespace Tradexa.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
        Task<InvoiceDto> GetByIdAsync(Guid id);
        Task<InvoiceDto> CreateAsync(InvoiceCreateDto dto);
        Task<InvoiceDto> UpdateAsync(Guid id, InvoiceUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
