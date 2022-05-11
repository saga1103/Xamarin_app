using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Gms.Maps;
using AndroidX.AppCompat.App;
using Android.Gms.Location;
using Xamarin.Essentials;
using System;
using Android.Gms.Tasks;
using Android.Gms.Maps.Model;

namespace Test_0
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity,IOnMapReadyCallback
    {
        TextView txtv;
        double lat, lng;

        protected override async void OnCreate(Bundle savedInstanceState) //async를 써서 oncreate를  비동기 메서드로 만든다.

        {
            base.OnCreate(savedInstanceState); //??

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.activity_main); //xml을 화면에 뿌려줌
            
            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);// 아마도 Onreadymap을 호출함 


            var location = await Geolocation.GetLastKnownLocationAsync();// 마지막 위치를 location에 저장 
           
            //예외처리를 위해서 try catch 문을 사용
            try
            {

                if (location != null)
                {
                    //위도 경도값을 따로 저장
                    lat = location.Latitude;
                    lng = location.Longitude;

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
           



            //버튼이벤트 연결부
            txtv = FindViewById<TextView>(Resource.Id.textView1);
            FindViewById<Button>(Resource.Id.button1).Click += (o, e) =>
            //txtv.Text = (++number).ToString();
            txtv.Text = location.Latitude.ToString();
           

        }
        //처음부터있던거임....  OnRequestPermissionsResult()
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        
        
        //map 설정
        public void OnMapReady(GoogleMap map)
        {
            //marker 의 옵션 설정
            MarkerOptions mo = new MarkerOptions();
            LatLng test = new LatLng(lat, lng);
           
            mo.SetPosition(test);
            mo.SetTitle("test");
            map.AddMarker(mo);
        }
    }
}