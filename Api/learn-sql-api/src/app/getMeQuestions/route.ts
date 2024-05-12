import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";

export async function POST(req: Request) {
  const { level, tag } = await req.json();

  if (level == null || !tag) {
    return NextResponse.json({ status: "fail", msg: "Invalid Params" });
  }

  try {
    const ret = await prisma.questionData.findMany({
      where: {
        level: level,
        tag: tag,
      },
      orderBy: {
        id: "asc",
      },
    });

    return NextResponse.json({ ststus: "success", ret });
  } catch (e) {
    return NextResponse.json({ status: "fail", e });
  }
}
