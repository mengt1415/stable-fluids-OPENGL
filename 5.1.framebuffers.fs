#version 330 core
out vec4 FragColor;
in vec2 TexCoords;
uniform sampler2D texture1;

void  main()
{
	FragColor=texture(texture1,TexCoords);
	vec4 color=texture(texture1,TexCoords);
	//FragColor=vec4(color.x-50.f,color.y-50.f,color.z-50.f,color.w);
}