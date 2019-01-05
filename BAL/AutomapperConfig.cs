using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using R.BusinessEntities;
using DAL;

namespace R.BAL
{
    //public static class MappingExpressionExtensions
    //{
    //    public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
    //    {
    //        expression.ForAllMembers(opt => opt.Ignore());
    //        return expression;
    //    }
    //}
    public static class AutoMapperConfiguration
    {
        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;
        }

        public static void Configure()
        {
            //Mapper.CreateMap<ta_vb_City, CityEntity>().IgnoreAllUnmapped()
            //        .ForMember(dto => dto.cityid, options => options.MapFrom(m => m.cityid))
            //        .ForMember(dto => dto.cityname, options => options.MapFrom(m => m.cityname))
            //        ;


            Mapper.CreateMap<ta_vbn_countries, CountryEntity>().IgnoreAllUnmapped()
                   .ForMember(dto => dto.coid, options => options.MapFrom(m => m.coid))
                   .ForMember(dto => dto.coname, options => options.MapFrom(m => m.coname))
                   .ForMember(dto => dto.shortname, options => options.MapFrom(m => m.shortname))
                   ;

            Mapper.CreateMap<ta_vbn_states, StateEntity>().IgnoreAllUnmapped()
                   .ForMember(dto => dto.stateid, options => options.MapFrom(m => m.stateid))
                   .ForMember(dto => dto.statename, options => options.MapFrom(m => m.statename))
                   .ForMember(dto => dto.coid, options => options.MapFrom(m => m.coid))
                   .ForMember(dto => dto.RelatedCountry, options => options.MapFrom(m => m.ta_vbn_countries))

                   ;
            Mapper.CreateMap<ta_vb_City, CityEntity>().IgnoreAllUnmapped()
                    .ForMember(dto => dto.cityid, options => options.MapFrom(m => m.cityid))
                    .ForMember(dto => dto.cityname, options => options.MapFrom(m => m.cityname))
                    .ForMember(dto => dto.stateid, options => options.MapFrom(m => m.stateid))
                    .ForMember(dto => dto.RelatedState, options => options.MapFrom(m => m.ta_vbn_states))

                    ;


            Mapper.CreateMap<ta_vb_Color, ColorEntity>().IgnoreAllUnmapped()
                    .ForMember(dto => dto.colorid, options => options.MapFrom(m => m.colorid))
                    .ForMember(dto => dto.colorname, options => options.MapFrom(m => m.colorname))
                    .ForMember(dto => dto.colorcode, options => options.MapFrom(m => m.colorcode))
                    ;

            Mapper.CreateMap<ta_vb_FuelType, FuelTypeEntity>().IgnoreAllUnmapped()
                   .ForMember(dto => dto.fuelid, options => options.MapFrom(m => m.fuelid))
                   .ForMember(dto => dto.fueltype, options => options.MapFrom(m => m.fueltype))
                   ;
            Mapper.CreateMap<ta_vb_VehicleType, VehicleTypeEntity>().IgnoreAllUnmapped()
                 .ForMember(dto => dto.vtypeid, options => options.MapFrom(m => m.vtypeid))
                 .ForMember(dto => dto.vtype, options => options.MapFrom(m => m.vtype))
                 ;

            Mapper.CreateMap<ta_vb_make, MakeEntity>().IgnoreAllUnmapped()
                   .ForMember(dto => dto.vmakeid, options => options.MapFrom(m => m.vmakeid))
                   .ForMember(dto => dto.vmakename, options => options.MapFrom(m => m.vmakename))
                   .ForMember(dto => dto.vmpublished, options => options.MapFrom(m => m.vmpublished))
                   .ForMember(dto => dto.vvehicletypeid, options => options.MapFrom(m => m.vvehicletypeid))

                   ;
            Mapper.CreateMap<ta_vb_Model, ModelEntity>().IgnoreAllUnmapped()
                  .ForMember(dto => dto.modelid, options => options.MapFrom(m => m.modelid))
                  .ForMember(dto => dto.modelname, options => options.MapFrom(m => m.modelname))
                  .ForMember(dto => dto.makeid, options => options.MapFrom(m => m.makeid))
                  ;

            Mapper.CreateMap<ta_vb_Varient, VarientEntity>().IgnoreAllUnmapped()
                  .ForMember(dto => dto.varientid, options => options.MapFrom(m => m.varientid))
                  .ForMember(dto => dto.varientname, options => options.MapFrom(m => m.varientname))
                  .ForMember(dto => dto.modelid, options => options.MapFrom(m => m.modelid))
                  ;

            Mapper.CreateMap<ta_vb_Image, ImageEntity>().IgnoreAllUnmapped()
                 .ForMember(dto => dto.imageid, options => options.MapFrom(m => m.imageid))
                 .ForMember(dto => dto.imagename, options => options.MapFrom(m => m.imagename))
                 .ForMember(dto => dto.imgtype, options => options.MapFrom(m => m.imgtype))
                 .ForMember(dto => dto.imagedata, options => options.MapFrom(m => m.imagedata))
                 .ForMember(dto => dto.vehicleid, options => options.MapFrom(m => m.vehicleid))
                 ;

            Mapper.CreateMap<ta_vb_advertisement, AdvertisementEntity>().IgnoreAllUnmapped()
                 .ForMember(dto => dto.addvid, options => options.MapFrom(m => m.addvid))
                 .ForMember(dto => dto.addvimage, options => options.MapFrom(m => m.addvimage))
                 .ForMember(dto => dto.addvpublish, options => options.MapFrom(m => m.addvpublish))
                 .ForMember(dto => dto.addvtext, options => options.MapFrom(m => m.addvtext))
                 .ForMember(dto => dto.addvcreatedate, options => options.MapFrom(m => m.addvcreatedate))
                 ;

            Mapper.CreateMap<ta_vb_Address, Address>().IgnoreAllUnmapped()
                    .ForMember(dto => dto.CityDetail, options => options.MapFrom(m => m.ta_vb_City))
                    .ForMember(dto => dto.a_city_id, options => options.MapFrom(m => m.a_city_id))
                    .ForMember(dto => dto.a_country, options => options.MapFrom(m => m.a_country))
                    .ForMember(dto => dto.a_dealercompany, options => options.MapFrom(m => m.a_dealercompany))
                    .ForMember(dto => dto.a_emailid, options => options.MapFrom(m => m.a_emailid))
                    .ForMember(dto => dto.a_mobile, options => options.MapFrom(m => m.a_mobile))
                    .ForMember(dto => dto.a_name, options => options.MapFrom(m => m.a_name))
                    .ForMember(dto => dto.a_pin, options => options.MapFrom(m => m.a_pin))
                    .ForMember(dto => dto.a_state, options => options.MapFrom(m => m.a_state))
                    .ForMember(dto => dto.address_id, options => options.MapFrom(m => m.address_id))
                    .ForMember(dto => dto.address1, options => options.MapFrom(m => m.address1))
                    .ForMember(dto => dto.address2, options => options.MapFrom(m => m.address2))
                    .ForMember(dto => dto.a_city, options => options.MapFrom(m => m.a_city))
                ;
            Mapper.CreateMap<ta_vb_User, UserProfileEntity>().IgnoreAllUnmapped()
                   .ForMember(dto => dto.UserAddress, options => options.MapFrom(m => m.ta_vb_Address))
                  .ForMember(dto => dto.address_id, options => options.MapFrom(m => m.address_id))
                  .ForMember(dto => dto.guser_id, options => options.MapFrom(m => m.guser_id))
                  .ForMember(dto => dto.guser_name, options => options.MapFrom(m => m.guser_name))
                  .ForMember(dto => dto.usertype, options => options.MapFrom(m => m.usertype))
                  ;

            Mapper.CreateMap<ta_vb_dealerproperty, DealerEntity>().IgnoreAllUnmapped()
                   .ForMember(dto => dto.DealerProfile, options => options.MapFrom(m => m.ta_vb_User))
                  .ForMember(dto => dto.dealerid, options => options.MapFrom(m => m.dealerid))
                  .ForMember(dto => dto.dealerpropid, options => options.MapFrom(m => m.dealerpropid))
                  .ForMember(dto => dto.dealerpublished, options => options.MapFrom(m => m.dealerpublished))
                  .ForMember(dto => dto.dpriority, options => options.MapFrom(m => m.dpriority))
                  .ForMember(dto => dto.drating, options => options.MapFrom(m => m.drating))
                  .ForMember(dto => dto.dremarks, options => options.MapFrom(m => m.dremarks))
                  .ForMember(dto => dto.requestdate, options => options.MapFrom(m => m.requestdate))
                  .ForMember(dto => dto.UserAddress, options => options.MapFrom(m => m.ta_vb_User.ta_vb_Address))
                   ;

            Mapper.CreateMap<ta_vb_rel_buyervehicle, RelationBuyerVehicleEntity>().IgnoreAllUnmapped()
               .ForMember(dto => dto.RelatedBuyer, options => options.MapFrom(m => m.ta_vb_User))
               //.ForMember(dto => dto.RelatedVehicle, options => options.MapFrom(m => m.ta_vb_Vehicle))
               .ForMember(dto => dto.bvrelid, options => options.MapFrom(m => m.bvrelid))
               .ForMember(dto => dto.fbuyerid, options => options.MapFrom(m => m.fbuyerid))
                .ForMember(dto => dto.fvehicleid, options => options.MapFrom(m => m.fvehicleid))
               .ForMember(dto => dto.requestdate, options => options.MapFrom(m => m.requestdate))
               .ForMember(dto => dto.requeststatus, options => options.MapFrom(m => m.requeststatus))

               ;

            Mapper.CreateMap<ta_vb_master_commisssion, MasterCommission>().IgnoreAllUnmapped()
             .ForMember(dto => dto.vsproptypeid, options => options.MapFrom(m => m.vsproptypeid))
             .ForMember(dto => dto.vsproptypename, options => options.MapFrom(m => m.vsproptypename))
             .ForMember(dto => dto.vsproptypedescription, options => options.MapFrom(m => m.vsproptypedescription))
             .ForMember(dto => dto.vcomissionpercent, options => options.MapFrom(m => m.vcomissionpercent))
             ;

            Mapper.CreateMap<ta_vb_sale, SaleEntity>().IgnoreAllUnmapped()
                .ForMember(dto => dto.saleid, options => options.MapFrom(m => m.saleid))
                .ForMember(dto => dto.saleprice, options => options.MapFrom(m => m.saleprice))
                .ForMember(dto => dto.saleremarks, options => options.MapFrom(m => m.saleremarks))
                .ForMember(dto => dto.saledate, options => options.MapFrom(m => m.saledate))
                .ForMember(dto => dto.submitdate, options => options.MapFrom(m => m.submitdate))
                .ForMember(dto => dto.Commission, options => options.MapFrom(m => m.ta_vb_master_commisssion))
                .ForMember(dto => dto.Dealer, options => options.MapFrom(m => m.ta_vb_User))
                .ForMember(dto => dto.Buyer, options => options.MapFrom(m => m.ta_vb_User1))
                //.ForMember(dto => dto.Vehicle, options => options.MapFrom(m => m.ta_vb_Vehicle))
                ;

            Mapper.CreateMap<ta_vb_VehicleDealerChecks, VehicleDealerChecks>().IgnoreAllUnmapped()
            .ForMember(dto => dto.vdcid, options => options.MapFrom(m => m.vdcid))
            .ForMember(dto => dto.vdcvehicleid, options => options.MapFrom(m => m.vdcvehicleid))
            .ForMember(dto => dto.vdcdealerprice, options => options.MapFrom(m => m.vdcdealerprice))
            .ForMember(dto => dto.vdcchecked, options => options.MapFrom(m => m.vdcchecked))
            .ForMember(dto => dto.vdealerremarks, options => options.MapFrom(m => m.vdealerremarks))

            ;

            Mapper.CreateMap<ta_vb_Vehicle, VehicleEntity>().IgnoreAllUnmapped()
                .ForMember(u => u.userid, op => op.MapFrom(t => t.userid))
                .ForMember(u => u.vexpectedprice, op => op.MapFrom(t => t.vexpectedprice))
                .ForMember(u => u.vkm, op => op.MapFrom(t => t.vkm))
                .ForMember(u => u.vmfgyear, op => op.MapFrom(t => t.vmfgyear))
                .ForMember(u => u.vtype, op => op.MapFrom(t => t.vtype))
                .ForMember(u => u.vmake, op => op.MapFrom(t => t.vmake))
                .ForMember(u => u.vmodel, op => op.MapFrom(t => t.vmodel))
                .ForMember(u => u.vavrient, op => op.MapFrom(t => t.vavrient))
                .ForMember(u => u.vcity, op => op.MapFrom(t => t.vcity))
                .ForMember(u => u.vcolor, op => op.MapFrom(t => t.vcolor))
                .ForMember(u => u.vfueltype, op => op.MapFrom(t => t.vfueltype))
                .ForMember(u => u.RelatedUser, op => op.MapFrom(t => t.ta_vb_User))
                .ForMember(u => u.VehicleCity, op => op.MapFrom(t => t.ta_vb_City))
                .ForMember(u => u.VehicleColor, op => op.MapFrom(t => t.ta_vb_Color))
                .ForMember(u => u.Vehiclemake, op => op.MapFrom(t => t.ta_vb_make))
                .ForMember(u => u.VehicleModel, op => op.MapFrom(t => t.ta_vb_Model))
                .ForMember(u => u.VehicleVarient, op => op.MapFrom(t => t.ta_vb_Varient))
                .ForMember(u => u.VehicleVehicleType, op => op.MapFrom(t => t.ta_vb_VehicleType))
                .ForMember(u => u.VehicleFuelType, op => op.MapFrom(t => t.ta_vb_FuelType))
                .ForMember(u => u.vehicleid, op => op.MapFrom(t => t.vehicleid))
                .ForMember(u => u.VehicleImages, op => op.MapFrom(t => t.ta_vb_Image))
                .ForMember(u => u.vcreatedate, op => op.MapFrom(t => t.vcreatedate))
                 .ForMember(u => u.vupdatedate, op => op.MapFrom(t => t.vupdatedate))
                 .ForMember(u => u.vshowhide, op => op.MapFrom(t => t.vshowhide))
                 .ForMember(u => u.SaleDetail, op => op.MapFrom(t => t.ta_vb_sale))
                 .ForMember(u => u.DealerChecks, op => op.MapFrom(t => t.ta_vb_VehicleDealerChecks))
                 .ForMember(u => u.vehiclesubmitted, op => op.MapFrom(t => t.vehiclesubmitted))
                 .ForMember(u => u.vsubmitdate, op => op.MapFrom(t => t.vsubmitdate))
                 .ForMember(u => u.vnoofowners, op => op.MapFrom(t => t.vnoofowners))

                .ForMember(u => u.RelatedBuyer, op => op.MapFrom(t => t.ta_vb_rel_buyervehicle))
                
                   ;

            Mapper.CreateMap<ta_vb_rel_dealervehicle, RelationDealerVehicleEntity>().IgnoreAllUnmapped()
                  .ForMember(dto => dto.DealerProfile, options => options.MapFrom(m => m.ta_vb_User))
                  .ForMember(dto => dto.VehicleDetail, options => options.MapFrom(m => m.ta_vb_Vehicle))
                  .ForMember(dto => dto.fdealerid, options => options.MapFrom(m => m.fdealerid))
                  .ForMember(dto => dto.fvehicleid, options => options.MapFrom(m => m.fvehicleid))
                  .ForMember(dto => dto.assigneddate, options => options.MapFrom(m => m.assigneddate))
                  ;
            Mapper.CreateMap<ta_vb_AdminSettings, PublicSettings>().IgnoreAllUnmapped()
                 .ForMember(dto => dto.sid, options => options.MapFrom(m => m.sid))
                 .ForMember(dto => dto.shuffle, options => options.MapFrom(m => m.shuffleoff))
                 ;
       

          
        }

        
    }
}
