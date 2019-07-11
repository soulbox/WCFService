using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceHost.AsansörveIşıklar
{
    using Extensions;
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class AsansorveIsıklar : PLCBase, IAsansor_Isıklar_IsıNem, IAsansor_Isıklar_IsıNemApi
    {
        private static readonly List<IAsansor_Isıklar_IsıNemCallback> CallbackChannels = new List<IAsansor_Isıklar_IsıNemCallback>();
        private static IAsansor_Isıklar_IsıNemCallback CurrentChannel;
        public AsansorveIsıklar()
        {
            PLC.CycleAsansör += PLC_CycleAsansör;
            PLC.CycleLight += PLC_CycleLight;
            PLC.CycleIsıNem += PLC_CycleIsıNem;
        }

        private void PLC_CycleIsıNem(PLC Cycle)
        {

            IsıNem  = Cycle;
           
            CallbackChannels.ToList()
                .ForEach(channel =>
                {
                    if (((ICommunicationObject)channel).State == CommunicationState.Opened)
                    {
                        try
                        {

                            channel.IsınemCycleHandler(Cycle);

                        }

                        catch (CommunicationException ex)
                        {
                            if (ex.ToString().Contains("System.IO.PipeException"))
                            {
                                CallbackChannels.Remove(channel);
                                Console.WriteLine($"Açılan Client Bağlantısı Kapatılıyor.\nState:{((ICommunicationObject)channel).State }".AppendLog());

                            }
                            else
                            {
                                Console.WriteLine($"Asanser Cycle Gönderilemedi.\nState:{((ICommunicationObject)channel).State }".AppendLog());

                                //throw new Exception(ex.ToString());
                            }

                        }
                        catch (System.TimeoutException)
                        {

                            CallbackChannels.Remove(channel);
                            Console.WriteLine($"Açılan Client Bağlantısı Kesildi/Zaman Aşımına uğradı.".AppendLog());
                        }

                    }
                });
        }

        private void PLC_CycleLight(PLC Cycle)
        {
            Işıklar = Cycle;
            CallbackChannels.ToList()
              .ForEach(channel =>
              {
                  if (((ICommunicationObject)channel).State == CommunicationState.Opened)
                  {
                      try
                      {
                          channel.LightCycleHandler(Cycle);

                      }

                      catch (CommunicationException ex)
                      {
                          if (ex.ToString().Contains("System.IO.PipeException"))
                          {
                              CallbackChannels.Remove(channel);
                              Console.WriteLine($"Açılan Client Bağlantısı Kapatılıyor.\nState:{((ICommunicationObject)channel).State }".AppendLog());

                          }
                          else
                          {
                              Console.WriteLine($"Asanser Cycle Gönderilemedi.\nState:{((ICommunicationObject)channel).State }".AppendLog());

                              //throw new Exception(ex.ToString());
                          }

                      }
                      catch (System.TimeoutException)
                      {

                          CallbackChannels.Remove(channel);
                          Console.WriteLine($"Açılan Client Bağlantısı Kesildi/Zaman Aşımına uğradı.".AppendLog());
                      }

                  }
              });
        }

        private void PLC_CycleAsansör(PLC Cycle)
        {
            Asansör = Cycle;
            CallbackChannels.ToList()
                .ForEach(channel =>
            {
                if (((ICommunicationObject)channel).State == CommunicationState.Opened)
                {
                    try
                    {

                        channel.AsansörCycleHandler(Cycle);

                    }

                    catch (CommunicationException ex)
                    {
                        if (ex.ToString().Contains("System.IO.PipeException"))
                        {
                            CallbackChannels.Remove(channel);
                            Console.WriteLine($"Açılan Client Bağlantısı Kapatılıyor.\nState:{((ICommunicationObject)channel).State }".AppendLog());

                        }
                        else
                        {
                            Console.WriteLine($"Asanser Cycle Gönderilemedi.\nState:{((ICommunicationObject)channel).State }".AppendLog());

                            //throw new Exception(ex.ToString());
                        }

                    }
                    catch (System.TimeoutException)
                    {

                        CallbackChannels.Remove(channel);
                        Console.WriteLine($"Açılan Client Bağlantısı Kesildi/Zaman Aşımına uğradı.".AppendLog());
                    }

                }
            });
        }

        public bool isClientReg()
        {
            var channel = OperationContext.Current.GetCallbackChannel<IAsansor_Isıklar_IsıNemCallback>();
            if (!CallbackChannels.Contains(channel))
            {
                CallbackChannels.Add(channel);
                CurrentChannel = channel;
                return true;
            }
            else
            {
                return true;
            }

        }

        public PLC GetStatus()
        {
            return PLC;
        }
        string geçersiz = $"Geçersiz Bir Başvuru!{Environment.NewLine}İşlem Yapılamadı";
       
        public object GetPropValue(string name)
        {
            try
            {
                
                return PLC[name];
            }
            catch (Exception) { return geçersiz; }
        }

        public object SetPropValue(string name, object value)
        {
            try
            {
               
                PLC[name] = value;    
                return PLC[name];
            }
            catch (Exception) { return geçersiz; }
        }        


    }
    [ServiceContract(CallbackContract = typeof(IAsansor_Isıklar_IsıNemCallback))]
    public interface IAsansor_Isıklar_IsıNem
    {
        [OperationContract]
        bool isClientReg();
    }
    [ServiceContract]
    public interface IAsansor_Isıklar_IsıNemApi : IPLCBase
    {


    }
    [ServiceContract]

    public interface IAsansor_Isıklar_IsıNemCallback
    {
        [OperationContract(IsOneWay = true)]
        void AsansörCycleHandler(PLC plc);
        [OperationContract(IsOneWay = true)]
        void LightCycleHandler(PLC plc);
        [OperationContract(IsOneWay = true)]
        void IsınemCycleHandler(PLC plc);
    }
}
