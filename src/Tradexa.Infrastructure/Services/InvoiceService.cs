using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradexa.Application.DTOs;
using Tradexa.Application.Interfaces;
using Tradexa.Domain.Entities;
using Tradexa.Infrastructure.Persistence;

namespace Tradexa.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Items)
                .ThenInclude(i => i.Product)
                .ToListAsync();

            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        public async Task<InvoiceDto> GetByIdAsync(Guid id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(i => i.Id == id);

            return invoice is null ? null : _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<InvoiceDto> CreateAsync(InvoiceCreateDto dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);
            invoice.Id = Guid.NewGuid();
            // invoice.CreatedAt = DateTime.UtcNow;

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<InvoiceDto> UpdateAsync(Guid id, InvoiceUpdateDto dto)
        {
            var invoice = await _context.Invoices.Include(i => i.Items).FirstOrDefaultAsync(i => i.Id == id);
            if (invoice == null) return null;

            // replace invoice items
            _context.InvoiceItems.RemoveRange(invoice.Items);
            _mapper.Map(dto, invoice);

            await _context.SaveChangesAsync();
            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) return false;

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
