using identitytest.Entities;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using identitytest.Infrastructre;

namespace identitytest.repository
{
    public static class AudiencesStore
    {
        //public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        
        static AudiencesStore()
        {
            //AudiencesList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
            //                    new Audience
            //                    {
            //                        ClientId = "099153c2625149bc8ecb3e85e03f0022",
            //                        Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
            //                        Name = "ResourceServer.Api 1"
            //                    });
        }

        public static Audience AddAudience(string name)
        {
            var contxt = new IdentDbContext();
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            Audience newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            contxt.Audience.Add(newAudience);
            contxt.SaveChanges();
            //AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        public static Audience FindAudience(string clientId)
        {
            Audience audience = null;
            var contxt = new IdentDbContext();
            audience = contxt.Audience.Where(a => a.ClientId == clientId).FirstOrDefault<Audience>();
            if (audience!=null)
            {
                return audience;
            }
            return null;
        }
    }
}