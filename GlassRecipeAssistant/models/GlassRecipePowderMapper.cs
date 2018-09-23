using GlassRecipeAssistant;
using GlassRecipeAssistant.dao;
using GlassRecipeAssistant.dao.entities;
using GlassRecipeAssistant.dao.implementions;
using GlassRecipeAssistant.GlassRecipesDatabaseDataSetTableAdapters;
using GlassRecipeAssistant.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAssistant.models
{
    public class GlassRecipePowderMapper : IGlassRecipePowderMapper
    {
        private IGlassRecipeRepository<GlassRecipe> grRepository;
        private IPowderRepository<Powder> powderRepository;

        public GlassRecipePowderMapper()
        {
            grRepository = RepositoryFactory.getGlassRecipeRepository();
            powderRepository = RepositoryFactory.getPowderRepository();
        }

        public List<GlassRecipeVo> search(string matchStr)
        {
            List<GlassRecipeVo> res = new List<GlassRecipeVo>();

            if (matchStr == null || matchStr.Equals(""))
            { // means find all
                findAll()
                    .ForEach(vo =>
                res.Add(vo));
            }
            else
            { // should match
                findAll()
                .Where(vo =>
                vo.Customer.Contains(matchStr)
                || vo.GlassName.Contains(matchStr)
                || vo.PowderName.Contains(matchStr)
                || vo.Weight.ToString().Contains(matchStr))
                .ToList()
                .ForEach(vo =>
                res.Add(vo));
            }
            return res;
        }

        /// <summary>
        /// Be careful! this method can act as save or update. So if you just
        /// want to save, make sure you have used contains() method before.
        /// </summary>
        /// <param name="vo"></param>
        public void save(GlassRecipeVo vo)
        {
            // check exists
            powderRepository.save(toPowder(vo));
            grRepository.save(toGlassRecipe(vo));
        }

        public void delete(GlassRecipeVo vo)
        {
            grRepository.delete(toGlassRecipe(vo));
        }

        public void deleteAll()
        {
            grRepository.deleteAll();
        }

        public bool existsById(int id)
        {
            throw new NotImplementedException();

        }

        public List<string> findCustomers()
        {
            return grRepository.findAll()
                .Select(gr => gr.Customer)
                .Distinct()
                .ToList();
        }


        public List<string> findGlassesByCustomer(string customer)
        {
            return grRepository.findByCustomer(customer)
                .Select(gr => gr.Glass)
                .Distinct()
                .ToList();
        }

        public Dictionary<string, double> findPowders(string customer, string glass)
        {
            Dictionary<string, double> res = new Dictionary<string, double>();

            grRepository.findByCustomerAndGlass(customer, glass)
                    .ForEach(gr =>
                    {
                        Optional<Powder> powder = powderRepository.findById(gr.PowderId);
                        if (powder.isPresent())
                        {
                            res.Add(powder.get().PowderName, gr.Weight);
                        }
                    });
            return res;
        }

        public void saveAll(List<GlassRecipeVo> vos)
        {
            List<GlassRecipe> grs = new List<GlassRecipe>();
            List<Powder> powders = new List<Powder>();

            // should save powder first to generate powder id
            vos.ForEach(vo => powders.Add(toPowder(vo)));
            powderRepository.saveAll(powders);

            // here would use powder id generated above
            vos.ForEach(vo => grs.Add(toGlassRecipe(vo)));
            grRepository.saveAll(grs);
        }

        private Powder toPowder(GlassRecipeVo vo)
        {
            Optional<Powder> powder = powderRepository.findByPowderName(vo.PowderName);
            if (powder.isPresent())
            {
                return powder.get();
            }
            else
            {
                //Powder newPowder = new Powder(vo.PowderName);
                //powderRepository.save(newPowder);
                //Optional<Powder> p = powderRepository.findByPowderName(vo.PowderName);
                //return p.get();
                return new Powder(vo.PowderName);
            }
        }

        private GlassRecipe toGlassRecipe(GlassRecipeVo vo)
        {
            return new GlassRecipe(
                vo.Customer,
                vo.GlassName,
                toPowder(vo).Id,
                vo.Weight
                );
        }

        private GlassRecipeVo toVo(GlassRecipe gr, Powder powder)
        {
            return new GlassRecipeVo(gr.Customer, gr.Glass,
                    powder.PowderName, gr.Weight);
        }

        public Optional<GlassRecipeVo> findById(int id)
        {
            throw new NotImplementedException();
        }

        public List<GlassRecipeVo> findAll()
        {
            List<GlassRecipeVo> res = new List<GlassRecipeVo>();

            grRepository.findAll()
                .ForEach(gr =>
                {
                    Optional<Powder> powder = powderRepository.findById(gr.PowderId);
                    if (powder.isPresent())
                    {
                        res.Add(toVo(gr, powder.get()));
                    }
                    else
                    {
                        res.Add(toVo(gr, new Powder("")));
                    }
                });

            return res;
        }

        public int count()
        {
            return grRepository.count();
        }

        public void deleteById(int id)
        {
            throw new NotImplementedException();
        }

        public bool existsByCustomerAndGlassAndPowder(string customer, string glass, string powder)
        {
            return findAll()
                .Any(v => v.Customer == customer
                && v.GlassName == glass
                && v.PowderName == powder);
        }

        public Optional<GlassRecipeVo> findByCustomerAndGlassAndPowder(string customer, string glass, string powder)
        {
            var query = findAll()
                .Where(vo => vo.Customer == customer
                && vo.GlassName == glass
                && vo.PowderName == powder)
                .FirstOrDefault();

            return new Optional<GlassRecipeVo>(query == null ? null : query);
        }
    }
}
