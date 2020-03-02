﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.StartScreen;
using Microsoft.VisualBasic;
using Microsoft.Toolkit.Uwp.Notifications;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace True_Love
{
    public sealed class CustomControl1 : Control
    {
        public CustomControl1()
        {
            this.DefaultStyleKey = typeof(CustomControl1);
        }

        private async void PinTile()
        {
            SecondaryTile tile = new SecondaryTile(DateTime.Now.Ticks.ToString())
            {
                DisplayName = "Xbox",
                Arguments = "args"
            };
            tile.VisualElements.Square150x150Logo = Constants.Square150x150Logo;
            tile.VisualElements.Wide310x150Logo = Constants.Wide310x150Logo;
            tile.VisualElements.Square310x310Logo = Constants.Square310x310Logo;
            tile.VisualElements.ShowNameOnSquare150x150Logo = true;
            tile.VisualElements.ShowNameOnSquare310x310Logo = true;
            tile.VisualElements.ShowNameOnWide310x150Logo = true;

            if (!await tile.RequestCreateAsync())
            {
                return;
            }

            // Generate the tile notification content and update the tile
            TileContent content = GenerateTileContent("MasterHip", "Assets/Photos/Owl.jpg");
            TileUpdateManager.CreateTileUpdaterForSecondaryTile(tile.TileId).Update(new TileNotification(content.GetXml()));
        }

        public static TileContent GenerateTileContent(string username, string avatarLogoSource)
        {
            return new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileMedium = GenerateTileBindingMedium(username, avatarLogoSource),
                    TileWide = GenerateTileBindingWide(username, avatarLogoSource),
                    TileLarge = GenerateTileBindingLarge(username, avatarLogoSource)
                }
            };
        }

        private static TileBinding GenerateTileBindingMedium(string username, string avatarLogoSource)
        {
            return new TileBinding()
            {
                Content = new TileBindingContentAdaptive()
                {
                    PeekImage = new TilePeekImage()
                    {
                        Source = avatarLogoSource,
                        HintCrop = TilePeekImageCrop.Circle
                    },

                    TextStacking = TileTextStacking.Center,

                    Children =
            {
                new AdaptiveText()
                {
                    Text = "Hi,",
                    HintAlign = AdaptiveTextAlign.Center,
                    HintStyle = AdaptiveTextStyle.Base
                },

                new AdaptiveText()
                {
                    Text = username,
                    HintAlign = AdaptiveTextAlign.Center,
                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                }
            }
                }
            };
        }

        private static TileBinding GenerateTileBindingWide(string username, string avatarLogoSource)
        {
            return new TileBinding()
            {
                Content = new TileBindingContentAdaptive()
                {
                    Children =
            {
                new AdaptiveGroup()
                {
                    Children =
                    {
                        new AdaptiveSubgroup()
                        {
                            HintWeight = 33,

                            Children =
                            {
                                new AdaptiveImage()
                                {
                                    Source = avatarLogoSource,
                                    HintCrop = AdaptiveImageCrop.Circle
                                }
                            }
                        },

                        new AdaptiveSubgroup()
                        {
                            HintTextStacking = AdaptiveSubgroupTextStacking.Center,

                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = "Hi,",
                                    HintStyle = AdaptiveTextStyle.Title
                                },

                                new AdaptiveText()
                                {
                                    Text = username,
                                    HintStyle = AdaptiveTextStyle.SubtitleSubtle
                                }
                            }
                        }
                    }
                }
            }
                }
            };
        }

        private static TileBinding GenerateTileBindingLarge(string username, string avatarLogoSource)
        {
            return new TileBinding()
            {
                Content = new TileBindingContentAdaptive()
                {
                    TextStacking = TileTextStacking.Center,

                    Children =
            {
                new AdaptiveGroup()
                {
                    Children =
                    {
                        new AdaptiveSubgroup()
                        {
                            HintWeight = 1
                        },

                        // We surround the image by two subgroups so that it doesn't take the full width
                        new AdaptiveSubgroup()
                        {
                            HintWeight = 2,
                            Children =
                            {
                                new AdaptiveImage()
                                {
                                    Source = avatarLogoSource,
                                    HintCrop = AdaptiveImageCrop.Circle
                                }
                            }
                        },

                        new AdaptiveSubgroup()
                        {
                            HintWeight = 1
                        }
                    }
                },

                new AdaptiveText()
                {
                    Text = "Hi,",
                    HintAlign = AdaptiveTextAlign.Center,
                    HintStyle = AdaptiveTextStyle.Title
                },

                new AdaptiveText()
                {
                    Text = username,
                    HintAlign = AdaptiveTextAlign.Center,
                    HintStyle = AdaptiveTextStyle.SubtitleSubtle
                }
            }
                }
            };
        }
    }
}
