const path = require("path");
const webpack = require("webpack");

module.exports = {
  mode: 'production',
  resolve: {
    extensions: [".ts", ".js"]
  },
  devtool: "inline-source-map",
  module: {
    rules: [
      {
        test: /\.ts?$/,
        loader: "ts-loader"
      }
    ]
  },
  entry: {
    "blazor.essentials": "./src/BlazorEssentials.ts"
  },
  output: {
    path: path.join(__dirname, "../DemoApp/wwwroot/scripts"),
    filename: "[name].js"
  }
};
