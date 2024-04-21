using Base.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataAccess.Cfg;

public class UserToMerchCfg : IEntityTypeConfiguration<UserToMerch>
{
    public void Configure(EntityTypeBuilder<UserToMerch> builder)
    {
        builder.HasKey(x => new { x.UserId, x.MerchId, x.PurchasedAt });
    }
}
