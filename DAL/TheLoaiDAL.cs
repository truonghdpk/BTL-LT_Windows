﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class TheLoaiDAL
    {
        dbDataContext data = new dbDataContext();

        public Object loadFromDB()
        {
            return data.TheLoais.Select(x => x).Select(x => new { x.MaTheLoai, x.TenTheLoai, x.GhiChu });
        }
        public Boolean SaveToDB(TheLoaiDTO newTheLoai)
        {
            // check if doc gia exist
            TaiLieu taiLieuORM = new TaiLieu();
            taiLieuORM.MaTaiLieu = newTheLoai.MaTheLoai;
            taiLieuORM.TenTaiLieu = newTheLoai.TenTheLoai;
            var docGias = data.TaiLieus.Single(x => x.MaTheLoai == taiLieuORM.MaTheLoai);
            if (docGias.MaTaiLieu.Length > 0)
            {
                // update 
                try
                {
                    this.UpdateToDB(newTheLoai);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
            else
            {
                // create
                try
                {
                    this.AddNewToDB(newTheLoai);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public Boolean AddNewToDB(TheLoaiDTO newTheLoai)
        {
            TheLoai theLoaiORM = new TheLoai();
            theLoaiORM.MaTheLoai = newTheLoai.MaTheLoai;
            theLoaiORM.TenTheLoai = newTheLoai.TenTheLoai;
            data.TheLoais.InsertOnSubmit(theLoaiORM);
            data.SubmitChanges();
            return true;
        }

        public Boolean UpdateToDB(TheLoaiDTO theLoai)
        {
            var line = data.TheLoais.Single(x => x.MaTheLoai == theLoai.MaTheLoai);
            line.MaTheLoai = theLoai.MaTheLoai;
            line.TenTheLoai = theLoai.TenTheLoai;
            data.SubmitChanges();
            return true;
        }
    }
}
