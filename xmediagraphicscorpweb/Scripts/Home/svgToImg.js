function svgToImg(svgNode) {

    console.log(svgNode);

    let xml = new XMLSerializer().serializeToString(svgNode);

    console.log(xml);

}