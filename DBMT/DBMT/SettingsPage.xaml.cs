using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using DBMT;
using Microsoft.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DBMT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            ReadSettingsFromConfig();
            SetDefaultBackGroundImage();
        }

        private void SetDefaultBackGroundImage()
        {
            string AssetsFolderPath = PathHelper.GetAssetsFolderPath();
            string imagePath = Path.Combine(AssetsFolderPath, "HomePageBackGround_DIY.png");
            if (!File.Exists(imagePath))
            {
                imagePath = Path.Combine(AssetsFolderPath, "HomePageBackGround.png");
            }

            // 创建 BitmapImage 并设置 ImageSource
            BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
            SettingsBGImageBrush.ImageSource = bitmap;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // 执行你想要在这个页面被关闭或导航离开时运行的代码
            SaveSettingsToConfig();
            // 如果需要，可以调用基类的 OnNavigatedFrom 方法
            base.OnNavigatedFrom(e);
        }

        public void SaveSettingsToConfig()
        {
            MainConfig.SetConfig("AutoCleanLogFile", ToggleSwitch_AutoCleanLogFile.IsOn);
            MainConfig.SetConfig("LogFileReserveNumber", (int)NumberBox_LogFileReserveNumber.Value);
            MainConfig.SetConfig("AutoCleanFrameAnalysisFolder", ToggleSwitch_AutoCleanFrameAnalysisFolder.IsOn);
            MainConfig.SetConfig("FrameAnalysisFolderReserveNumber", (int)NumberBox_FrameAnalysisFolderReserveNumber.Value);
            MainConfig.SetConfig("ModelFileNameStyle", ComboBox_ModelFileNameStyle.SelectedIndex);

            MainConfig.SetConfig("MoveIBRelatedFiles", ToggleSwitch_MoveIBRelatedFiles.IsOn);
            MainConfig.SetConfig("DontSplitModelByMatchFirstIndex", ToggleSwitch_DontSplitModelByMatchFirstIndex.IsOn);

            MainConfig.SetConfig("GenerateSeperatedMod", ToggleSwitch_GenerateSeperatedMod.IsOn);
            MainConfig.SetConfig("Author", TextBox_Author.Text);
            MainConfig.SetConfig("AuthorLink", TextBox_AuthorLink.Text);
            MainConfig.SetConfig("ModSwitchKey", TextBox_ModSwitchKey.Text);

            MainConfig.SetConfig("AutoTextureFormat", ComboBox_AutoTextureFormat.SelectedIndex);
            MainConfig.SetConfig("AutoTextureOnlyConvertDiffuseMap", ToggleSwitch_AutoTextureOnlyConvertDiffuseMap.IsOn);
            MainConfig.SetConfig("ConvertDedupedTextures", ToggleSwitch_ConvertDedupedTextures.IsOn);
            MainConfig.SetConfig("ForbidMoveTrianglelistTextures", ToggleSwitch_ForbidMoveTrianglelistTextures.IsOn);
            MainConfig.SetConfig("ForbidMoveDedupedTextures", ToggleSwitch_ForbidMoveDedupedTextures.IsOn);
            MainConfig.SetConfig("ForbidMoveRenderTextures", ToggleSwitch_ForbidMoveRenderTextures.IsOn);
            MainConfig.SetConfig("ForbidAutoTexture", ToggleSwitch_ForbidAutoTexture.IsOn);
            MainConfig.SetConfig("UseHashTexture", ToggleSwitch_UseHashTexture.IsOn);

            MainConfig.SaveAllConfig();
        }

        public void ReadSettingsFromConfig()
        {
            //防止程序启动时没正确读取，这里冗余读取一次，后面看情况可以去掉。
            MainConfig.LoadConfigFromFiles();

            ToggleSwitch_AutoCleanLogFile.IsOn = MainConfig.GetConfig<bool>("AutoCleanLogFile");
            NumberBox_LogFileReserveNumber.Value = MainConfig.GetConfig<int>("LogFileReserveNumber");
            ToggleSwitch_AutoCleanFrameAnalysisFolder.IsOn = MainConfig.GetConfig<bool>("AutoCleanFrameAnalysisFolder");
            NumberBox_FrameAnalysisFolderReserveNumber.Value = MainConfig.GetConfig<int>("FrameAnalysisFolderReserveNumber");
            ComboBox_ModelFileNameStyle.SelectedIndex = MainConfig.GetConfig<int>("ModelFileNameStyle");
            ToggleSwitch_MoveIBRelatedFiles.IsOn = MainConfig.GetConfig<bool>("MoveIBRelatedFiles");
            ToggleSwitch_DontSplitModelByMatchFirstIndex.IsOn = MainConfig.GetConfig<bool>("DontSplitModelByMatchFirstIndex");

            ToggleSwitch_GenerateSeperatedMod.IsOn = MainConfig.GetConfig<bool>("GenerateSeperatedMod");
            TextBox_Author.Text = MainConfig.GetConfig<string>("Author");
            TextBox_AuthorLink.Text = MainConfig.GetConfig<string>("AuthorLink");
            TextBox_ModSwitchKey.Text = MainConfig.GetConfig<string>("ModSwitchKey");

            ComboBox_AutoTextureFormat.SelectedIndex = MainConfig.GetConfig<int>("AutoTextureFormat");
            ToggleSwitch_AutoTextureOnlyConvertDiffuseMap.IsOn = MainConfig.GetConfig<bool>("AutoTextureOnlyConvertDiffuseMap");
            ToggleSwitch_ConvertDedupedTextures.IsOn = MainConfig.GetConfig<bool>("ConvertDedupedTextures");
            ToggleSwitch_ForbidMoveTrianglelistTextures.IsOn = MainConfig.GetConfig<bool>("ForbidMoveTrianglelistTextures");
            ToggleSwitch_ForbidMoveDedupedTextures.IsOn = MainConfig.GetConfig<bool>("ForbidMoveDedupedTextures");
            ToggleSwitch_ForbidMoveRenderTextures.IsOn = MainConfig.GetConfig<bool>("ForbidMoveRenderTextures");
            ToggleSwitch_ForbidAutoTexture.IsOn = MainConfig.GetConfig<bool>("ForbidAutoTexture");
            ToggleSwitch_UseHashTexture.IsOn = MainConfig.GetConfig<bool>("UseHashTexture");
        }

    }
}
