using System.Linq;
using System.Data;
using System.Data.Entity;
using Kendo.Mvc.Extensions;

using Hermes.DAL;



namespace Hermes.BPM
{

    public static class Moria
    {
        public static int MoriaSocial(mor_AITISI_DATA aitisiData)
        {
            int moria_social = 0;
            int moria_father_anapiria = 0;
            int moria_mother_anapiria = 0;
            int moria_child_amea = 0;
            int moria_child_orphan = 0;
            int moria_divorced = 0;
            int moria_parent_army = 0;

            if (aitisiData == null)
                return moria_social;

            if (aitisiData.FATHER_DISABILITY == true) moria_father_anapiria = 30;
            if (aitisiData.MOTHER_DISABILITY == true) moria_mother_anapiria = 30;
            if (aitisiData.CHILD_AMEA == true) moria_child_amea = 50;
            if (aitisiData.CHILD_ORPHAN == true) moria_child_orphan = 40;
            if (aitisiData.PARENT_DIVORCED == true) moria_divorced = 20;
            if (aitisiData.PARENT_INARMY == true) moria_parent_army = 10;

            moria_social = moria_father_anapiria + moria_mother_anapiria + moria_child_amea + moria_child_orphan + moria_divorced + moria_parent_army;

            return moria_social;
        }

        public static int MoriaOaed(mor_AITISI_DATA aitisiData) 
        {
            int moria_oaed = 0;

            if (aitisiData == null)
                return moria_oaed;

            if (aitisiData.DIKAIOYXOI_BOTH == true) moria_oaed = 10;

            return moria_oaed;
        }

        public static int MoriaIncome(mor_AITISI_DATA aitisiData)
        {
            int moria_income = 0;

            if (aitisiData == null)
                return moria_income;

            using (var db = new HermesDBEntities())
            {
                // FAMILY_INCOME is an alias for INCOME_CATEGORY in query
                var data = (from d in db.FAMILY_INCOME where d.INCOME_ID == aitisiData.FAMILY_INCOME select d).FirstOrDefault();
                if (data == null)
                    return moria_income;

                moria_income = (int)data.INCOME_MORIA;

                return moria_income;
            }
        }

        public static int MoriaChildren(mor_AITISI_DATA aitisiData)
        {
            int moria_children = 0;

            if (aitisiData == null)
                return moria_children;

            if (aitisiData.CHILDREN_NUMBER == 3) moria_children = 15;
            else if (aitisiData.CHILDREN_NUMBER == 4) moria_children = 20;
            else if (aitisiData.CHILDREN_NUMBER >= 5) moria_children = 40;

            return moria_children;
        }

        public static int MoriaRegister(mor_AITISI_DATA aitisiData)
        {
            int moria_re_register = 0;

            if (aitisiData == null)
                return moria_re_register;

            if (aitisiData.RE_REGISTRATION == true) 
                moria_re_register = 35;

            return moria_re_register;
        }

        public static int MoriaSibling(mor_AITISI_DATA aitisiData)
        {
            int moria_sibling = 0;

            if (aitisiData == null)
                return moria_sibling;

            // λογική να μη μοριοδοτείται εάν είναι επανεγγραφή και έχει αδελφάκι 
            // μικρότερο που κάνει εγγραφή για πρώτη φορά.

            if (aitisiData.SIBLING_IN_BNS == true && aitisiData.RE_REGISTRATION == false)
                moria_sibling = 10;

            return moria_sibling;
        }

        public static int MoriaTotal(mor_AITISI_DATA aitisiData)
        {
            int moria_total = 0;

            moria_total = MoriaSocial(aitisiData) + MoriaOaed(aitisiData) + MoriaIncome(aitisiData) + 
                          MoriaChildren(aitisiData) + MoriaRegister(aitisiData) + MoriaSibling(aitisiData);

            return moria_total;
        }

        public static int WorkingMotherRanking(mor_AITISI_DATA src)
        {
            int rank = 1;
            if (src.WORKING_MOTHER == 0)
                rank = 2;

            return rank;
        }

        public static void ComputeAitisiMoria(int aitisiId)
        {
            using (var db = new HermesDBEntities())
            {
                var src = (from d in db.mor_AITISI_DATA where d.AITISI_ID == aitisiId select d).FirstOrDefault();

                AITISIS aitisi = db.AITISIS.Find(aitisiId);
                {
                    aitisi.MORIA_SOCIAL = MoriaSocial(src);
                    aitisi.MORIA_OAED = MoriaOaed(src);
                    aitisi.MORIA_INCOME = MoriaIncome(src);
                    aitisi.MORIA_FAMILY = MoriaChildren(src);
                    aitisi.MORIA_REREGISTER = MoriaRegister(src);
                    aitisi.MORIA_SIBLING = MoriaSibling(src);
                    aitisi.MORIA_TOTAL = MoriaTotal(src);
                    aitisi.RANKING = aitisi.RANKING > 0 ? aitisi.RANKING : WorkingMotherRanking(src);
                };
                db.Entry(aitisi).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }   // class Moria

}   // namespace